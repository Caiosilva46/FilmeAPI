using FilmesAPI.Interface;
using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Repositorio
{
    public class RepositorioLocacao : IRepositorioLocacao
    {

        string connectionString = @"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        SqlDataReader dataRead = null;

        public List<Locacao> RetornaLocacao()
        {
            string queryString = @"SELECT Id, titulo, valor, dataRetirada, dataDevolucao, filmeId, clienteId FROM tb_locacao";
            Locacao locacao;
            List<Locacao> ListaLocacao = new List<Locacao>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        locacao = new Locacao
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Titulo = reader["titulo"].ToString(),
                            Valor = Convert.ToDecimal(reader["valor"].ToString()),
                            DataRetirada = Convert.ToDateTime(reader["dataRetirada"].ToString()),
                            DataDevolucao = Convert.ToDateTime(reader["dataDevolucao"].ToString()),
                            ClienteId = Convert.ToInt32(reader["clienteId"].ToString()),
                            FilmeId = Convert.ToInt32(reader["filmeId"].ToString())
                        };

                        ListaLocacao.Add(locacao);
                    }
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
                    else if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }
            return ListaLocacao;
        }


        public Locacao RetornaLocacaoId(int id)
        {
            string queryString = @"SELECT Id, titulo, valor, dataRetirada, dataDevolucao, filmeId, clienteId FROM tb_locacao where id = @id";
            Locacao locacao = null;

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
                        locacao = new Locacao
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            Titulo = reader["titulo"].ToString(),
                            Valor = Decimal.Parse(reader["valor"].ToString()),
                            DataRetirada = Convert.ToDateTime(reader["dataRetirada"].ToString()),
                            DataDevolucao = Convert.ToDateTime(reader["dataDevolucao"].ToString()),
                            ClienteId = int.Parse(reader["clienteId"].ToString()),
                            FilmeId = int.Parse(reader["filmeId"].ToString())
                        };
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                return locacao;
            }
        }

        public void AdicionaLocacao(Locacao locacao)
        {
            string queryString = @"INSERT INTO tb_locacao (Id, titulo, valor, dataRetirada, dataDevolucao) VALUES (@Id, @titulo, @valor, @dataRetirada, @dataDevolucao)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", locacao.Id);
                    command.Parameters.AddWithValue("@titulo", locacao.Titulo);
                    command.Parameters.AddWithValue("@valor", locacao.Valor);
                    command.Parameters.AddWithValue("@dataRetirada", DateTime.Now.ToShortDateString());
                    command.Parameters.AddWithValue("@dataDevolucao", DateTime.Now.ToShortDateString());
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

        public void AtualizaLocacao(Locacao locacao)
        {
            string queryString = @"UPDATE tb_locacao SET titulo = @titulo, valor = @valor, dataRetirada = @dataRetirada, dataDevolucao = @dataDevolucao WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", locacao.Id);
                    command.Parameters.AddWithValue("@titulo", locacao.Titulo);
                    command.Parameters.AddWithValue("@valor", locacao.Valor);
                    command.Parameters.AddWithValue("@dataRetirada", DateTime.Now.ToShortDateString());
                    command.Parameters.AddWithValue("@dataDevolucao", locacao.DataDevolucao);
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

        public void RemoveLocacao(int id)
        {
            string queryString = @"DELETE FROM tb_locacao WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", id);
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
