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

        public List<Cliente> GetCliente()
        {
            string queryString = @"SELECT c.id, c.nome, c.cpf, c.rg, c.email, c.senha, c.ativo, c.datacadastro FROM tb_cliente AS c";
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
                            Senha = reader["senha"].ToString(),
                            Ativo = Convert.ToBoolean(reader["ativo"]),
                            DataCadastrado = Convert.ToDateTime(reader["datacadastro"].ToString())
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

        public Cliente GetClienteById(int id)
        {
            Cliente cliente = null;
            string queryString = @"SELECT c.id, c.nome, c.cpf, c.rg, c.email, c.senha, c.ativo, c.datacadastro FROM tb_cliente AS c WHERE c.id = @id ";

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
                            Ativo = Convert.ToBoolean(reader["ativo"]),
                            DataCadastrado = Convert.ToDateTime(reader["datacadastro"].ToString())
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

        public void PostCliente(Cliente cliente)
        {
            string queryString = @"INSERT INTO tb_cliente (nome, cpf, rg, email, senha, ativo, datacadastro) VALUES (@nome ,@cpf, @rg, @email, @senha, @ativo, @datacadastro)";

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
                    command.Parameters.AddWithValue("@ativo", string.Join(",", cliente.Ativo));
                    command.Parameters.AddWithValue("@datacadastro", string.Join(",", DateTime.Now.ToShortDateString()));
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

        public void PutCliente(Cliente cliente)
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

        public void DeleteCliente(int id)
        {
            string queryString = @"UPDATE tb_cliente SET ativo = 0 WHERE id = @id";

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

        public bool GetId(int id)
        {
            string queryString = @"SELECT c.id FROM tb_cliente AS c WHERE id = @id";

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

        public bool GetCpf(string cpf)
        {
            string queryString = @"SELECT c.cpf FROM tb_cliente AS c WHERE cpf = @cpf";

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

        public bool GetEmail(string email)
        {
            string queryString = @"SELECT c.email FROM tb_cliente AS c WHERE email = @email";

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

        public bool GetSenha(string senha, string email)
        {
            string queryString = @"SELECT c.senha, c.email FROM tb_cliente AS c WHERE c.senha = @senha AND c.email = @email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool senhaCryptografada = false;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@senha", senha);
                command.Parameters.AddWithValue("@email", email);
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

        public bool GetStatusLocacao(int id)
        {
            string queryString = @"SELECT l.filmeid FROM tb_locacao AS l JOIN tb_filme f ON f.id = l.filmeid WHERE l.filmeid = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool statusLocacao = false;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    statusLocacao = true;
                }

                connection.Close();
                return statusLocacao;
            }
        }
    }
}



