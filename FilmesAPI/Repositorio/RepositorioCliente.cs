using FilmesAPI.Interface;
using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models.ValueObject;
using System.Data;

namespace FilmesAPI.Repositorio
{
    public class RepositorioCliente : IRepositorioCliente
    {
        SqlDataReader dataRead = null;

        string connectionString = @"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Cliente> RetornaCliente()
        {
            string queryString = @"SELECT c.id, c.nome, c.cpf, c.rg, c.email, c.senha FROM tb_cliente as c";
            Cliente cliente;
            List<Cliente> ListaDeClientes = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cliente = new Cliente
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            Nome = reader["nome"].ToString(),
                            Cpf = reader["cpf"].ToString(),
                            Rg = reader["rg"].ToString(),
                            Email = reader["email"].ToString(),
                            Senha = reader["senha"].ToString()
                        };
                        ListaDeClientes.Add(cliente);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dataRead != null)
                    {
                        dataRead.Close();
                    }

                    connection.Close();

                }
                return ListaDeClientes;
            }
        }

        public Cliente RetornaClienteId(int id)
        {
            Cliente cliente = null;
            string queryString = @"SELECT c.id, c.nome, c.cpf, c.rg, c.email, c.senha FROM tb_cliente as c WHERE c.id = @id ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cliente = new Cliente
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            Nome = reader["nome"].ToString(),
                            Cpf = reader["cpf"].ToString(),
                            Rg = reader["rg"].ToString(),
                            Email = reader["email"].ToString(),
                            Senha = reader["senha"].ToString(),
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dataRead != null)
                    {
                        dataRead.Close();
                    }

                    connection.Close();
                }
                return cliente;
            }
        }

        public void AdicionaCliente(Cliente cliente)
        {
            string queryString = @"INSERT INTO tb_cliente (nome, cpf, rg, email, senha) VALUES (@nome ,@cpf, @rg, @email, @senha)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@nome", string.Join(",", cliente.Nome));
                    command.Parameters.AddWithValue("@cpf", string.Join(",", cliente.Cpf));
                    command.Parameters.AddWithValue("@rg", string.Join(",", cliente.Rg));
                    command.Parameters.AddWithValue("@email", string.Join(",", cliente.Email));
                    command.Parameters.AddWithValue("@senha", string.Join(",", cliente.Senha));
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dataRead != null)
                    {
                        dataRead.Close();
                    }
                    connection.Close();
                }
            }
        }

        public void AtualizaCliente(Cliente cliente)
        {
            string queryString = @"UPDATE tb_cliente SET nome = @nome, email = @email WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@id", string.Join(",", cliente.Id));
                    command.Parameters.AddWithValue("@nome", string.Join(",", cliente.Nome));
                    command.Parameters.AddWithValue("@email", string.Join(",", cliente.Email));
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (dataRead != null)
                    {
                        dataRead.Close();
                    }

                    connection.Close();
                }
            }
        }

        public void RemoveCliente(int id)
        {
            string queryString = @"DELETE FROM tb_cliente WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (dataRead != null)
                    {
                        dataRead.Close();
                    }
                    connection.Close();
                }
            }
        }

        public bool LocalizaId(int id)
        {
            string queryString = @"SELECT c.id FROM tb_cliente as c WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool resultado = false;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    resultado = true;
                }

                connection.Close();
                return resultado;
            }
        }

        public bool CpfCadastrado(string cpf)
        {
            string queryString = @"SELECT c.cpf FROM tb_cliente as c WHERE cpf = @cpf";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool cpfCadastrado = false;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@cpf", cpf);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    cpfCadastrado = true;
                }

                connection.Close();
                return cpfCadastrado;
            }
        }

        public bool EmailCadastrado(string email)
        {
            string queryString = @"SELECT c.email FROM tb_cliente as c WHERE email = @email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool emailValido = false;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    emailValido = true;
                }

                connection.Close();
                return emailValido;
            }
        }

        public bool SenhaCadastrada(string senha)
        {
            string queryString = @"SELECT c.senha FROM tb_cliente as c WHERE senha = @senha";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool senhaCryptografada = false;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@senha", senha);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    senhaCryptografada = true;
                }

                connection.Close();
                return senhaCryptografada;
            }
        }
    }
}



