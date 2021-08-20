using FilmesAPI.Interface;
using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Repositorio
{
    public class RepositorioCliente : IRepositorioCliente
    {
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=everis;User ID=root;Password=1304");
      
        //SqlDataReader dr = null;
        public void AlteraCliente(Cliente cliente)
        {
            string AtualizaCliente = $@"update Cliente set Nome,CPF,RG,Email,Senha where '{cliente.Nome}','{cliente.CPF}',{cliente.RG}',{cliente.Email}','{cliente.Senha}'";
            SqlCommand cmd = new SqlCommand(AtualizaCliente);
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
        }

        public void InsereCliente(Cliente cliente)
        {
            string incluiCliente = $@"insert into Cliente (Nome, CPF, RG, Email, Senha) values ('{cliente.Nome}','{cliente.CPF}','{cliente.RG}', '{cliente.Email}', '{cliente.Senha}')";
            SqlCommand cmd = new SqlCommand(incluiCliente, conn);
            cmd.ExecuteNonQuery();
        }

        public void RemoverCliente(int id)
        {
            string excluiCliente = $@"delete from Cliente  where id == {id}";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = excluiCliente;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
        }

        public void RetornaCliente(int id)
        {
            SqlCommand cmd = new SqlCommand("select Id, Nome, CPF, RG, Email, Senha from Cliente", conn);
            SqlDataReader dr = cmd.ExecuteReader();
        }


        Cliente IRepositorioCliente.RetornaCliente(int id)
        {
            SqlCommand cmd = new SqlCommand("select Nome, CPF, RG, Email ,Senha from Cliente", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            return null;

        }
    }
}
