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

        public void AtualizaCliente(Cliente cliente)
        {
            string queryString = @"UPDATE tb_cliente SET nome = @nome, rg = @rg, email = @email, senha = @senha WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", cliente.Id);
                    command.Parameters.AddWithValue("@nome", cliente.Nome);
                    command.Parameters.AddWithValue("@cpf", cliente.Cpf);
                    command.Parameters.AddWithValue("@rg", cliente.RG);
                    command.Parameters.AddWithValue("@email", cliente.Email);
                    command.Parameters.AddWithValue("@senha", cliente.Senha);
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    if (dataRead != null)
                    {
                        dataRead.Close();
                    }
                    else if (connection != null)
                    {
                        connection.Close();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void AdicionaCliente(Cliente cliente)
        {
            string queryString = @"INSERT INTO tb_cliente (id, nome, cpf, rg, email, senha) VALUES (@id ,@nome ,@cpf, @rg, @email, @senha)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@id", cliente.Id);
                    command.Parameters.AddWithValue("@nome", cliente.Nome);
                    command.Parameters.AddWithValue("@cpf", cliente.Cpf);
                    command.Parameters.AddWithValue("@rg", cliente.RG);
                    command.Parameters.AddWithValue("@email", cliente.Email);
                    command.Parameters.AddWithValue("@senha", cliente.Senha);
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    if (cliente.Id == cliente.Id)
                    {
                        cliente.Id++;
                    }
                }
                finally
                {
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
                    if (connection == null)
                    {
                        connection.Close();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

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
                            Nome = reader["nome"].ToString(),
                            Cpf = Convert.ToString(reader["cpf"]),
                            RG = Convert.ToString(reader["rg"]),
                            Email = Convert.ToString(reader["email"]),
                            Senha = reader["senha"].ToString(),
                            Id = int.Parse(reader["id"].ToString())
                        };
                        ListaDeClientes.Add(cliente);
                    }
                }
                finally
                {
                    if (dataRead != null)
                    {
                        dataRead.Close();
                    }
                    else if (connection != null)
                    {
                        connection.Close();
                    }
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
                            Cpf = Convert.ToString(reader["cpf"]),
                            RG = Convert.ToString(reader["rg"]),
                            Email = Convert.ToString(reader["email"]),
                            Senha = reader["senha"].ToString(),
                        };
                    }
                }
                catch (Exception)
                {
                    if (dataRead != null)
                    {
                        dataRead.Close();
                    }
                    else if (connection != null)
                    {
                        connection.Close();
                    }
                }
                finally
                {
                    connection.Close();
                }
                return cliente;
            }
        }

        public bool CpfCadastrado(string cpf)
        {
            string queryString = @"SELECT cpf FROM tb_cliente WHERE cpf = @cpf";

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

        public bool LoginCliente(string senha, string email)
        {
            string queryString = @"SELECT email, senha FROM tb_cliente WHERE email = @email AND senha = @senha";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                bool logado = false;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@senha", senha);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    logado = true;
                }

                connection.Close();
                return logado;
            }
        }

        public bool LocalizaId(int id)
        {
            string queryString = @"SELECT id FROM tb_cliente WHERE id = @id";

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

        public bool EmailCadastrado(string email)
        {
            string queryString = @"SELECT email FROM tb_cliente WHERE email = @email";

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

        public bool Senhahash(string senha)
        {
            string queryString = @"SELECT senha FROM tb_cliente WHERE senha = @senha";

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



