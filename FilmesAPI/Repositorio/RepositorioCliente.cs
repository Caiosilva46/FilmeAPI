using FilmesAPI.Interface;
using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models.ValueObject;

namespace FilmesAPI.Repositorio
{
    public class RepositorioCliente : IRepositorioCliente
    {
        SqlDataReader dr = null;
        public void AtualizaCliente(Cliente cliente)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            try
            {
                conn.Open();
                string AtualizaCliente = @"UPDATE Cliente SET Nome = @Nome, CPF = @CPF, RG = @RG, Email = @Email, Senha = @Senha WHERE ClienteId = @ClienteId";
                SqlCommand cmd = new SqlCommand(AtualizaCliente, conn);
                cmd.Parameters.AddWithValue("@ClienteId", cliente.ClienteId);
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
                cmd.Parameters.AddWithValue("@RG", cliente.RG);
                cmd.Parameters.AddWithValue("@Email", cliente.email);
                cmd.Parameters.AddWithValue("@Senha", cliente.Senha);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex; // retorna mensagem de erro
            }

            finally
            {
                conn.Close();
            }

        }

        public void AdicionaCliente(Cliente cliente)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            try
            {
                conn.Open();
                string InsereCliente = @"INSERT INTO cliente (ClienteId, Nome, CPF, RG, Email, Senha) VALUES (@ClienteId ,@Nome, @CPF, @RG, @Email, @Senha)";
                SqlCommand cmd = new SqlCommand(InsereCliente, conn);
                cmd.Parameters.AddWithValue("@ClienteId", cliente.ClienteId);
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@CPF", cliente.Cpf); 
                cmd.Parameters.AddWithValue("@RG", cliente.RG);
                cmd.Parameters.AddWithValue("@Email", cliente.email);
                cmd.Parameters.AddWithValue("@Senha", cliente.Senha);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;  // retorna mensagem de erro
            }

            finally
            {
                conn.Close();
            }
        }

        public void RemoveCliente(int id)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            try
            {
                conn.Open();
                string RemoveCliente =  @"DELETE  FROM cliente WHERE ClienteId = @ClienteId";
                SqlCommand cmd = new SqlCommand(RemoveCliente, conn);
                cmd.Parameters.AddWithValue("@ClienteId", id);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;  // retorna mensagem de erro
            }

            finally
            {
                conn.Close();
            }
        }

        public List<Cliente> RetornaCliente()
        {

            SqlConnection conn = new SqlConnection(@"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            Cliente cliente = null;
            List<Cliente> ListCliente = new List<Cliente>();

            try
            {
                conn.Open();
                string retornaCliente = @"SELECT * FROM cliente";
                SqlCommand cmd = new SqlCommand(retornaCliente, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cliente = new Cliente();
                    cliente.Nome = dr["Nome"].ToString();
                    cliente.Cpf = dr["CPF"].ToString();
                    cliente.RG = dr["RG"].ToString();
                    cliente.email = dr["Email"].ToString();
                    cliente.Senha = dr["Senha"].ToString();
                    cliente.ClienteId = Convert.ToInt32(dr["ClienteId"]);
                    ListCliente.Add(cliente);
                }
            }
            
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                else if (conn != null)
                {
                    conn.Close();
                }
            }
            return ListCliente;
        }

        public Cliente RetornaClienteId(int id)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            Cliente cliente = null;

            try
            {
                conn.Open();
                string RetornaClienteId = @"SELECT * FROM Cliente WHERE ClienteId = " + id;
                SqlCommand cmd = new SqlCommand(RetornaClienteId, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cliente = new Cliente();
                    cliente.ClienteId = Convert.ToInt32(dr["ClienteId"].ToString());
                    cliente.Nome = dr["Nome"].ToString();
                    cliente.Cpf = dr["CPF"].ToString();
                    cliente.RG = dr["RG"].ToString();
                    cliente.email = dr["Email"].ToString();
                    cliente.Senha = dr["Senha"].ToString();
                }
            }

            catch (Exception ex)
            {
                throw ex;  // retorna mensagem de erro
            }

            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                else if (conn != null)
                {
                    conn.Close();
                }
            }
            return cliente;
        }

        public void CpfCadastrado(CPF cpf)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            try
            {
                conn.Open();
                string RecuperaCPF = @"SELECT CPF FROM Cliente WHERE CPF = @CPF";
                SqlCommand cmd = new SqlCommand(RecuperaCPF, conn);
                cmd.Parameters.AddWithValue("@CPF", cpf);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;  // retorna mensagem de erro
            }

            finally
            {
                conn.Close();
            }
        }

    }
}

