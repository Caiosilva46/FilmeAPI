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

        public List<Filme> GetFilme()
        {
            string queryString = @"SELECT f.id, f.titulo, f.genero, f.qtdestoque, f.ativo, f.datacadastro FROM tb_filme AS f";
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
                            Id = Convert.ToInt32(reader["id"].ToString()),
                            Titulo = reader["titulo"].ToString(),
                            Genero = reader["genero"].ToString(),
                            QtdEstoque = Convert.ToInt32(reader["qtdestoque"].ToString()),
                            Ativo = Convert.ToBoolean(reader["ativo"].ToString()),
                            DataCadastro = Convert.ToDateTime(reader["datacadastro"].ToString()),

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

        public Filme GetFilmeById(int id)
        {
            string queryString = @"SELECT f.id, f.titulo, f.genero, f.qtdestoque, f.ativo, f.datacadastro FROM tb_filme AS f WHERE id = @id";
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
                            QtdEstoque = Convert.ToInt32(reader["qtdestoque"].ToString()),
                            Ativo = Convert.ToBoolean(reader["ativo"].ToString()),
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

        public void PostFilme(Filme filme)
        {
            string queryString = @"INSERT INTO tb_filme (titulo, genero, qtdestoque, ativo, datacadastro) VALUES (@titulo, @genero, @qtdestoque, @ativo, @datacadastro)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@titulo", filme.Titulo);
                    command.Parameters.AddWithValue("@genero", filme.Genero);
                    command.Parameters.AddWithValue("@qtdestoque", filme.QtdEstoque);
                    command.Parameters.AddWithValue("@ativo", filme.Ativo);
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

        public void PutFilme(Filme filme)
        {
            string queryString = @"UPDATE tb_filme SET titulo = @titulo, genero = @genero, qtdestoque = @qtdestoque WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@id", filme.Id);
                    command.Parameters.AddWithValue("@titulo", filme.Titulo);
                    command.Parameters.AddWithValue("@genero", filme.Genero);
                    command.Parameters.AddWithValue("@qtdestoque", filme.QtdEstoque);
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

        public void DeleteFilme(int id)
        {
            string queryString = @"UPDATE tb_filme SET ativo = 0 WHERE id = @id";

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

        public bool GetId(int id)
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

        public bool GetTitulo(Filme filme)
        {
            string queryString = @"SELECT f.titulo, f.genero FROM tb_filme AS f WHERE f.titulo = @titulo AND f.genero = @genero";

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

        public bool GetStatusLocacao(int id)
        {
            string queryString = @"SELECT l.filmeid FROM tb_locacao AS l JOIN tb_locacao c ON c.id = l.filmeid WHERE l.filmeid = @id";

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

