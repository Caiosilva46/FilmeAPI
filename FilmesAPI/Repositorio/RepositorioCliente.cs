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
            string queryString = @"UPDATE Cliente SET Nome = @Nome, CPF = @CPF, RG = @RG, Email = @Email, Senha = @Senha WHERE ClienteId = @ClienteId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@ClienteId", cliente.ClienteId);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@CPF", cliente.Cpf);
                    command.Parameters.AddWithValue("@RG", cliente.RG);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Senha", cliente.Senha);
                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex; // retorna mensagem de erro
                }

                finally
                {
                    connection.Close();
                }
            }
        }

        public void AdicionaCliente(Cliente cliente)
        {
            string queryString = @"INSERT INTO cliente (ClienteId, Nome, CPF, RG, Email, Senha) VALUES (@ClienteId ,@Nome, @CPF, @RG, @Email, @Senha)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@ClienteId", cliente.ClienteId);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@CPF", cliente.Cpf);
                    command.Parameters.AddWithValue("@RG", cliente.RG);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Senha", cliente.Senha);
                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;  // retorna mensagem de erro
                }

                finally
                {
                    connection.Close();
                }
            }
        }

        public void RemoveCliente(int id)
        {
            string queryString = @"DELETE  FROM cliente WHERE ClienteId = @ClienteId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@ClienteId", id);
                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;  // retorna mensagem de erro
                }

                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Cliente> RetornaCliente()
        {
            string queryString = @"SELECT * FROM cliente";
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
                            Nome = reader["Nome"].ToString(),
                            //Cpf = (CPF)reader["CPF"],
                            Cpf = reader["CPF"].ToString(),
                            RG = reader["RG"].ToString(),
                            //Email = (Email)reader["Email"],
                            Email = reader["Email"].ToString(),

                            Senha = reader["Senha"].ToString(),
                            ClienteId = Convert.ToInt32(reader["ClienteId"])
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
            string queryString = @"SELECT * FROM Cliente WHERE ClienteId = " + id;

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
                            ClienteId = Convert.ToInt32(reader["ClienteId"].ToString()),
                            Nome = reader["Nome"].ToString(),
                            //Cpf = (CPF)reader["CPF"],
                            Cpf = reader["CPF"].ToString(),
                            RG = reader["RG"].ToString(),
                            //Email = (Email)reader["Email"],
                            Email = reader["Email"].ToString(),
                            Senha = reader["Senha"].ToString(),
                        };
                    }
                }

                catch (Exception ex)
                {
                    throw ex;  // retorna mensagem de erro
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

                return cliente;
            }
        }

        public void CpfCadastrado(CPF cpf)
        {
            string queryString = @"SELECT CPF FROM Cliente WHERE CPF = @CPF";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@CPF", cpf);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LendoCpf((IDataRecord)reader);
                }

                // usuário não existe

                reader.Close();

            }

            void LendoCpf(IDataRecord record)
            {
                if (cpf.Equals(record[0]))
                {
                    //throw new Exception(); aqui retorna a mensagem se o CPF existir
                }
            }
        }

        public void LoginCliente(Cliente cliente)
        {
            string queryString = @"SELECT Email, Senha FROM Usuarios WHERE Email = @Email AND Senha = @Senha";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@ClienteId", cliente.Email);
                    command.Parameters.AddWithValue("@ClienteId", cliente.Senha);
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }

                finally
                {
                    connection.Close();
                }
            }
        }
    }
}


