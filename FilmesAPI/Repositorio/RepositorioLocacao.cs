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

        public void AtualizaLocacao(Locacao locacao)
        {
            string queryString = @"UPDATE Locacao SET Titulo = @Titulo, Valor = @Valor, DataRetirada = @DataRetirada, DataDevolucao = @DataDevolucao WHERE LocacaoId = @LocacaoId=Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@LocacaoId", locacao.LocacaoId);
                    command.Parameters.AddWithValue("@Titulo", locacao.Titulo);
                    command.Parameters.AddWithValue("@Valor", locacao.Valor);
                    command.Parameters.AddWithValue("@DataRetirada", DateTime.Now.ToShortDateString());
                    command.Parameters.AddWithValue("@DataDevolucao", DateTime.Now.ToShortDateString());
                    command.ExecuteNonQuery();
                }

                catch (Exception)
                {
                    if(locacao.LocacaoId != locacao.LocacaoId)
                    {
                        throw new Exception();

                    }
                }

                finally
                {
                    connection.Close();
                }
            }
        }

        public void AdicionaLocacao(Locacao locacao)
        {
            string queryString = @"INSERT INTO locacao (LocacaoId, Titulo, Valor, DataRetirada, DataDevolucao) VALUES (@LocacaoId, @Titulo, @Valor, @DataRetirada, @DataDevolucao)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@LocacaoId", locacao.LocacaoId);
                    command.Parameters.AddWithValue("@Titulo", locacao.Titulo);
                    command.Parameters.AddWithValue("@Valor", locacao.Valor);
                    command.Parameters.AddWithValue("@DataRetirada", DateTime.Now.ToShortDateString());
                    command.Parameters.AddWithValue("@DataDevolucao", DateTime.Now.ToShortDateString());
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    if (locacao.LocacaoId == locacao.LocacaoId)
                    {
                        locacao.LocacaoId++;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void RemoveLocacao(int id)
        {
            string queryString = @"DELETE FROM locacao WHERE LocacaoId = @LocacaoId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@LocacaoId", id);
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

        public List<Locacao> RetornaLocacao()
        {
            string queryString = @"SELECT * FROM Locacao";
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
                            LocacaoId = Convert.ToInt32(reader["LocacaoId"]),
                            Titulo = reader["Titulo"].ToString(),
                            Valor = Convert.ToDecimal(reader["Valor"]),
                            DataRetirada = Convert.ToDateTime(reader["DataRetirada"]),
                            DataDevolucao = Convert.ToDateTime(reader["DataDevolucao"]),
                            ClienteId = Convert.ToInt32(reader["ClienteId"]),
                            FilmeId = Convert.ToInt32(reader["FilmeId"])
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
            string queryString = @"SELECT * FROM Locacao WHERE LocacaoId = " + id;
            Locacao locacao = null;

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
                            LocacaoId = Convert.ToInt32(reader["LocacaoId"]),
                            Titulo = reader["Titulo"].ToString(),
                            Valor = Convert.ToDecimal(reader["Valor"]),
                            DataRetirada = Convert.ToDateTime(reader["DataRetirada"]),
                            DataDevolucao = Convert.ToDateTime(reader["DataDevolucao"]),
                            ClienteId = Convert.ToInt32(reader["ClienteId"]),
                            FilmeId = Convert.ToInt32(reader["FilmeId"])
                        };

                    }
                }

                catch (Exception ex)
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

                return locacao;
            }
        }
    }
}
