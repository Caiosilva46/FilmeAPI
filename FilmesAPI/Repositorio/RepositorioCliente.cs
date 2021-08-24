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
        SqlDataReader dr = null;
        public void AlteraCliente(Cliente cliente)
        {
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=everis;Integrated Security=SSPI");

            try 
            {
                conn.Open();
                string AtualizaCliente = @"UPDATE Cliente SET Nome = @Nome, CPF = @CPF, RG = @RG, Email = @Email, Senha = @Senha WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(AtualizaCliente, conn);
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
                cmd.Parameters.AddWithValue("@RG", cliente.RG);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
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

        public void InsereCliente(Cliente cliente)
        {
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=everis;Integrated Security=SSPI");

            try 
            {
                conn.Open();
                string InsereCliente = @"INSERT INTO Cliente (Nome, CPF, RG, Email, Senha) VALEUS (@Nome, @CPF, @RG, @Email, @Senha)";
                SqlCommand cmd = new SqlCommand(InsereCliente, conn);
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@CPF", cliente.CPF); 
                cmd.Parameters.AddWithValue("@RG", cliente.RG);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
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

        public void RemoveCliente(Guid id)
        {
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=everis;Integrated Security=SSPI");

            try 
            {
                conn.Open();
                string RemoveCliente =  @"DELETE  FROM Cliente WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(RemoveCliente, conn);
                cmd.Parameters.AddWithValue("@Id", id);
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
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=everis;Integrated Security=SSPI");

            Cliente cliente = null;
            List<Cliente> ListCliente = new List<Cliente>();

            try
            {
                conn.Open();
                string retornaCliente = @$"SELECT * FROM Filme";
                SqlCommand cmd = new SqlCommand(retornaCliente, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cliente = new Cliente();
                    cliente.Nome = dr["Nome"].ToString();
                    cliente.CPF = dr["CPF"].ToString();
                    cliente.RG = dr["RG"].ToString();
                    cliente.Email = dr["Email"].ToString();
                    cliente.Senha = dr["Senha"].ToString();
                    cliente.Id = Guid.Parse(dr["Id"].ToString());
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

        public Cliente RetornaClienteId(Guid id)
        {
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=everis;Integrated Security=SSPI");

            try
            {
                conn.Open();
                string RetornaClienteId = @"SELECT Id FROM Cliente WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(RetornaClienteId, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                return null;
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

