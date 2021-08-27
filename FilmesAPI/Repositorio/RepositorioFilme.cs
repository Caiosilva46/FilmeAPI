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
        public void AtualizaFilme(Filme filme)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                conn.Open();
                string AtualizaFilme = @"UPDATE filme SET Titulo = @Titulo, Genero = @Genero, DataCadastro = @DataCadastro  WHERE FilmeId = @FilmeId";
                SqlCommand cmd = new SqlCommand(AtualizaFilme, conn);
                cmd.Parameters.AddWithValue("@FilmeID", filme.FilmeId);
                cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);
                cmd.Parameters.AddWithValue("@Genero", filme.Genero);
                cmd.Parameters.AddWithValue("@DataCadastro", filme.DataCadastro);

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

        public void AdicionaFilme(Filme filme)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            try
            {
                conn.Open();
                string AdicionaFilme = @"INSERT INTO filme (FilmeId, Titulo, Genero, DataCadastro) VALUES (@FilmeId, @Titulo, @Genero, @DataCadastro)";
                SqlCommand cmd = new SqlCommand(AdicionaFilme, conn);
                cmd.Parameters.AddWithValue("@FilmeId", filme.FilmeId);
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

        public void RemoveFilme(int id)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            try
            {
                conn.Open();
                string RemoveFilme = @"DELETE  FROM filme WHERE FilmeId = @FilmeId";
                SqlCommand cmd = new SqlCommand(RemoveFilme, conn);
                cmd.Parameters.AddWithValue("@FilmeId", id);
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

            SqlConnection conn = new SqlConnection(@"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            Filme filme = null;
            List<Filme> ListFilme = new List<Filme>();
            

            try
            {
                conn.Open();
                string retornaFilme = @$"SELECT * FROM filme";
                SqlCommand cmd = new SqlCommand(retornaFilme, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    filme = new Filme();
                    filme.Titulo = dr["Titulo"].ToString();
                    filme.Genero = dr["Genero"].ToString();
                    filme.DataCadastro = dr["DataCadastro"].ToString();
                    filme.FilmeId = Convert.ToInt32(dr["FilmeId"].ToString());
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

        public Filme RetornaFilmeId(int id)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            Filme filme = null;

            try
            {
                conn.Open();
                string RetornaFilmeId = @"SELECT * FROM Filme WHERE FilmeId = " + id;
                SqlCommand cmd = new SqlCommand(RetornaFilmeId, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    filme = new Filme();
                    filme.FilmeId = Convert.ToInt32(dr["FilmeId"]);
                    filme.Titulo = dr["Titulo"].ToString();
                    filme.Genero = dr["Genero"].ToString();
                    filme.DataCadastro = dr["DataCadastro"].ToString();
                }
            }

            catch (Exception ex)
            {
                throw ex;  // retorna mensagem de erro
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
            return filme;

        }
    }
}

