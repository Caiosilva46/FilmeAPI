﻿using FilmesAPI.Interface;
using FilmesAPI.Models;
using FilmesAPI.Models.ValueObject;
using FilmesAPI.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class ServiceCliente : IServiceCliente
    {
        private readonly RepositorioCliente rep = new RepositorioCliente();

        public void AtualizaCliente(Cliente cliente)
        {
            cliente.Senha = CrypSenha(cliente.Senha);

            rep.AtualizaCliente(cliente);
        }

        public void AdicionaCliente(Cliente cliente)
        {
            rep.AdicionaCliente(cliente);
        }

        public void RemoveCliente(int id)
        {
            rep.RemoveCliente(id);
        }

        public Cliente RetornaClienteId(int id)
        {
            return rep.RetornaClienteId(id);
        }

        public List<Cliente> RetornaCliente()
        {
            return rep.RetornaCliente();
        }

        public bool LocalizaId(int id)
        {
            return rep.LocalizaId(id);
        }

        public bool CpfCadastrado(string Cpf)
        {
            return rep.CpfCadastrado(Cpf);
        }

        public bool EmailCadastrado(string email)
        {
            return rep.EmailCadastrado(email);
        }

        public bool SenhaCadastrada(string senha)
        {
            return rep.SenhaCadastrada(senha);
        }

        public void ValidaCliente(Cliente cliente)
        {
            string CpfValido = cliente.Cpf.ToString();
            CPF cpf = new CPF();
            cliente.Cpf = CpfValido;

            string rgValido = cliente.Rg.ToString();
            //RG rg = new RG(rgValido);

            string emailValido = cliente.Email.ToString();
            Email email = new Email();

            if (!CpfCadastrado(CpfValido))
            {
                cliente.Senha = CrypSenha(cliente.Senha);
            }
        }

        public string CrypSenha(string senha)
        {
            MD5 md5Hash = MD5.Create();
            byte[] senhaMd = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < senhaMd.Length; i++)
            {
                sBuilder.Append(senhaMd[i].ToString("X2"));
            }

            return sBuilder.ToString();
        }

        public bool VerificarHash(string senha, string hash)
        {
            StringComparer compara = StringComparer.OrdinalIgnoreCase;

            if (compara.Compare(senha, hash).Equals(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
