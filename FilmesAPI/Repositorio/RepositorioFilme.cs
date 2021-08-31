using FilmesAPI.Interface;
using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Repositorio
{

    public class RepositorioFilme : IRepositorioFilme
    {
        string connectionString = @"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        SqlDataReader dataRead = null;
        public void AtualizaFilme(Filme filme)
        {
            string queryString = @"UPDATE filme SET Titulo = @Titulo, Genero = @Genero, DataCadastro = @DataCadastro  WHERE FilmeId = @FilmeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@FilmeID", filme.FilmeId);
                    command.Parameters.AddWithValue("@Titulo", filme.Titulo);
                    command.Parameters.AddWithValue("@Genero", filme.Genero);
                    command.Parameters.AddWithValue("@DataCadastro", DateTime.Now.ToShortDateString());
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

        public void AdicionaFilme(Filme filme)
        {

            string queryString = @"INSERT INTO filme (FilmeId, Titulo, Genero, DataCadastro) VALUES (@FilmeId, @Titulo, @Genero, @DataCadastro)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@FilmeId", filme.FilmeId);
                    command.Parameters.AddWithValue("@Titulo", filme.Titulo);
                    command.Parameters.AddWithValue("@Genero", filme.Genero);
                    command.Parameters.AddWithValue("@DataCadastro",  DateTime.Now.ToShortDateString());
                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    if (filme.FilmeId == filme.FilmeId)
                    {
                        filme.FilmeId++;
                    }
                }

                finally
                {
                    connection.Close();
                }
            }
        }

        public void RemoveFilme(int id)
        {
            string queryString = @"DELETE  FROM filme WHERE FilmeId = @FilmeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@FilmeId", id);
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

        public List<Filme> RetornaFilme()
        {

            string queryString = @$"SELECT * FROM filme";
            Filme filme;
            List<Filme> ListFilme = new List<Filme>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        filme = new Filme
                        {
                            Titulo = reader["Titulo"].ToString(),
                            Genero = reader["Genero"].ToString(),
                            DataCadastro = Convert.ToDateTime(reader["DataCadastro"].ToString()),
                            FilmeId = Convert.ToInt32(reader["FilmeId"].ToString())
                        };
                        ListFilme.Add(filme);
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
                return ListFilme;
            }

        }

        public Filme RetornaFilmeId(int id)
        {
            string queryString = @"SELECT * FROM Filme WHERE FilmeId = " + id;
            Filme filme = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        filme = new Filme
                        {
                            FilmeId = Convert.ToInt32(reader["FilmeId"]),
                            Titulo = reader["Titulo"].ToString(),
                            Genero = reader["Genero"].ToString(),
                            DataCadastro = Convert.ToDateTime(reader["DataCadastro"].ToString())
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

                return filme;

            }

        }
    }
}

