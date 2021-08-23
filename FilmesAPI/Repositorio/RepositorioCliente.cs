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
        SqlDataReader dr = null;
        public void AlteraCliente(Cliente cliente)
        {
            string AtualizaCliente = $@"UPDATE Cliente SET Nome, CPF, RG, Email, Senha WHERE Id = {cliente.Id}";
            SqlCommand cmd = new SqlCommand(AtualizaCliente);
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
        }

        public void InsereCliente(Cliente cliente)
        {
            string incluiCliente = $@"INSERT INTO Cliente (Nome, CPF, RG, Email, Senha) values ('{cliente.Nome}','{cliente.CPF}','{cliente.RG}', '{cliente.Email}', '{cliente.Senha}')";
            SqlCommand cmd = new SqlCommand(incluiCliente, conn);
            cmd.ExecuteNonQuery();
        }

        public void RemoverCliente(Guid id)
        {
            string excluiCliente = $@"DELETE FROM Cliente WHERE id = {id}";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = excluiCliente;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
        }

        public List<Cliente> RetornaCliente()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            return null;
        }

        public Cliente RetornaClienteId(Guid id)
        {
            SqlCommand cmd = new SqlCommand("select Nome, CPF, RG, Email ,Senha from Cliente", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            return null;
        }
    }
}
