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

        SqlDataReader dr = null;
        public void AlteraFilme(Filme filme)
        {
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=everis;Integrated Security=SSPI");

            try
            {
                conn.Open();
                string AtualizaFilme = @"UPDATE Filme SET Titulo = @Titulo, Genero = @Genero WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(AtualizaFilme, conn);
                cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);
                cmd.Parameters.AddWithValue("@Genero", filme.Genero);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex; // retorna mensagem de erro
            }

            finally
            {
                conn.Close();
            }
        }

        public void InsereFilme(Filme filme)
        {
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=everis;Integrated Security=SSPI");

            try 
            {
                conn.Open();
                string InsereFilme = @"INSERT INTO Filme (Titulo, Genero, DataCadastro) VALEUS (@Titulo, @Genero, @DataCadastro)";
                SqlCommand cmd = new SqlCommand(InsereFilme, conn);
                cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);
                cmd.Parameters.AddWithValue("@Genero", filme.Genero);
                cmd.Parameters.AddWithValue("@DataCadastro", filme.DataCadastro);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;  // retorna mensagem de erro
            }

            finally
            {
                conn.Close();
            }
        }

        public void RemoveFilme(Guid id)
        {
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=everis;Integrated Security=SSPI");


            try
            {
                conn.Open();
                string RemoveFilme = @"DELETE  FROM Filme WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(RemoveFilme, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;  // retorna mensagem de erro
            }

            finally
            {
                conn.Close();
            }
        }

        public List<Filme> RetornaFilme()
        {
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=everis;Integrated Security=SSPI");
            Filme filme = null;
            List<Filme> ListFilme = new List<Filme>();
            

            try
            {
                conn.Open();
                string retornaFilme = @$"SELECT * FROM Filme";
                SqlCommand cmd = new SqlCommand(retornaFilme, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    filme = new Filme();
                    filme.Titulo = dr["Titulo"].ToString();
                    filme.Genero = dr["Genero"].ToString();
                    filme.DataCadastro = dr["DataCadastro"].ToString();
                    filme.Id = Guid.Parse(dr["Id"].ToString());
                    ListFilme.Add(filme);
                }
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                else if (conn != null)
                {
                    conn.Close();
                }
            }
            return ListFilme;
        }

        public Filme RetornaFilmeId(Guid id)
        {
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=everis;Integrated Security=SSPI");

            try
            {
                conn.Open();
                string RetornaFilmeId = @"SELECT Id FROM Filme WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(RetornaFilmeId, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                return null;
            }

            catch (Exception ex)
            {
                throw ex;  // retorna mensagem de erro
            }

            finally
            {
                conn.Close();
            }

        }
    }
}

