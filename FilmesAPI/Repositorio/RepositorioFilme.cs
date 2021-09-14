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

        public List<Filme> RetornaFilme()
        {
            string queryString = @"SELECT f.id, f.titulo, f.genero, f.datacadastro  FROM tb_filme as f";
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
                            Titulo = reader["titulo"].ToString(),
                            Genero = reader["genero"].ToString(),
                            DataCadastro = Convert.ToDateTime(reader["datacadastro"].ToString()),
                            Id = Convert.ToInt32(reader["id"].ToString())
                        };
                        ListFilme.Add(filme);
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
                return ListFilme;
            }
        }

        public Filme RetornaFilmeId(int id)
        {
            string queryString = @"SELECT f.id, f.titulo, f.genero, f.datacadastro FROM tb_filme as f WHERE id = @id";
            Filme filme = null;

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
                        filme = new Filme
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Titulo = reader["titulo"].ToString(),
                            Genero = reader["genero"].ToString(),
                            DataCadastro = Convert.ToDateTime(reader["datacadastro"].ToString())
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
                return filme;
            }
        }

        public void AdicionaFilme(Filme filme)
        {
            string queryString = @"INSERT INTO tb_filme (titulo, genero, datacadastro) VALUES (@titulo, @genero, @datacadastro)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@titulo", filme.Titulo);
                    command.Parameters.AddWithValue("@genero", filme.Genero);
                    command.Parameters.AddWithValue("@datacadastro", DateTime.Now.ToShortDateString());
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

        public void AtualizaFilme(Filme filme)
        {
            string queryString = @"UPDATE tb_filme SET titulo = @titulo, genero = @genero  WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@id", filme.Id);
                    command.Parameters.AddWithValue("@titulo", filme.Titulo);
                    command.Parameters.AddWithValue("@genero", filme.Genero);
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

        public void RemoveFilme(int id)
        {
            string queryString = @"DELETE FROM tb_filme WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
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

        public bool LocalizaId(int id)
        {
            string queryString = @"SELECT id FROM tb_filme WHERE id = @id";

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

        public bool TituloCadastrado(Filme filme)
        {
            string queryString = @"SELECT titulo, genero FROM tb_filme WHERE titulo = @titulo and genero = @genero";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool filmeCadastrado = false;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@titulo", filme.Titulo);
                command.Parameters.AddWithValue("@genero", filme.Genero);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    filmeCadastrado = true;
                }

                connection.Close();

                return filmeCadastrado;
            }
        }
    }
}

