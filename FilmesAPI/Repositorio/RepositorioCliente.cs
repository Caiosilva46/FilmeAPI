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
        SqlDataReader dataRead;

        string connectionString = @"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void AtualizaCliente(Cliente cliente)
        {
            string queryString = @"UPDATE Cliente SET Nome = @Nome, RG = @RG, Email = @Email, Senha = @Senha WHERE ClienteId = @ClienteId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@ClienteId", cliente.ClienteId);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    //command.Parameters.AddWithValue("@CPF", cliente.Cpf);
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

                catch (Exception)
                {
                    if (cliente.ClienteId == cliente.ClienteId)
                    {
                        cliente.ClienteId++;
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
            string queryString = @"DELETE FROM cliente WHERE ClienteId = @ClienteId";

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
                    throw ex;
                }

                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Cliente> RetornaCliente()
        {
            string queryString = @"SELECT c.ClienteId, c.Nome, c.Cpf, c.RG, c.Email, c.Senha FROM cliente as c";
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
                            Cpf = Convert.ToString(reader["CPF"]),
                            RG = reader["RG"].ToString(),
                            Email = Convert.ToString(reader["Email"]),
                            Senha = reader["Senha"].ToString(),
                            ClienteId = int.Parse(reader["ClienteId"].ToString())
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
            string queryString = @"SELECT c.ClienteId, c.Nome, c.Cpf, c.RG, c.Email, c.Senha FROM Cliente as c WHERE c.ClienteId = @Id ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {

                        cliente = new Cliente
                        {
                            ClienteId = int.Parse(reader["ClienteId"].ToString()),
                            Nome = reader["Nome"].ToString(),
                            Cpf = Convert.ToString(reader["CPF"]),
                            RG = reader["RG"].ToString(),
                            Email = Convert.ToString(reader["Email"]),
                            Senha = reader["Senha"].ToString(),
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw;
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
                    throw new Exception("CPF existe!");
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

        public bool LocalizaId(int id)
        {
            string queryString = @"SELECT clienteId FROM Cliente WHERE clienteId = @clienteId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool resultado = false;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@ClienteId", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    resultado = Convert.ToBoolean(reader["ClienteId"]);

                }

                connection.Close();

                return resultado;
            }
        }
    }
}


