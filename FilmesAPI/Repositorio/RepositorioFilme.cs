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
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=everis;User ID=root;Password=1304");

        SqlDataReader dr = null;
        public void AlteraFilme(Filme filme)
        {
            string AtualizaFilme = $@"UPDATE Filme SET Titulo, Genero WHERE Id = {filme.Id} ";
            SqlCommand cmd = new SqlCommand(AtualizaFilme);
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
        }

        public void InsereFilme(Filme filme)
        {
            string IncluiFilme = $@"INSERT INTO Filme (Titulo, Genero, DataCadastro) VALUES ('{filme.Titulo}', '{filme.Genero}', '{filme.DataCadastro}')";
            SqlCommand cmd = new SqlCommand(IncluiFilme, conn);
            cmd.ExecuteNonQuery();
        }

        public void RemoveFilme(Guid id)
        {
            string excluiFilme = $@"DELETE FROM Filme WHERE id = {id}";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = excluiFilme;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
        }

        public List<Filme> RetornaFilme()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Filme", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            return Filme;
        }

        public Filme RetornaFilmeId(Guid id)
        {
            SqlCommand cmd = new SqlCommand($"SELECT id FROM Filme WHERE id = '{id}' ", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            return id;
        }
    }
}
