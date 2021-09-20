using FilmesAPI.Models;
using FilmesAPI.Models.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    public interface IRepositorioCliente
    {
        List<Cliente> GetCliente();

        Cliente GetClienteById(int id);

        void PostCliente(Cliente cliente);

        void PutCliente(Cliente cliente);

        void DeleteCliente(int id);

        bool GetId(int id);

        bool GetCpf(string cpf);

        bool GetEmail(string email);

        bool GetSenha(string senha, string email);

        bool GetStatusLocacao(int id);
    }
}
