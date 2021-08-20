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

        //SqlDataReader dr = null;
        public void AlteraFilme(Filme filme)
        {
            string AtualizaFilme = $@"update Filme set Titulo, genero where '{filme.Titulo}', {filme.Genero} ";
            SqlCommand cmd = new SqlCommand(AtualizaFilme);
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
        }

        public void InsereFilme(Filme filme)
        {
            string IncluiFilme = $@"insert into Filme (Titulo, Genero, DataCadastro) values ('{filme.Titulo}', '{filme.Genero}', '{filme.DataCadastro}')";
            SqlCommand cmd = new SqlCommand(IncluiFilme, conn);
            cmd.ExecuteNonQuery();
        }

        public void RemoveFilme(int id)
        {
            string excluiFilme = $@"delete from Filme where id == {id}";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = excluiFilme;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
        }

        public void RetornaFilme(int id)
        {
            SqlCommand cmd = new SqlCommand("select Id,Titulo, Genero, Datacadastro from Filme", conn);
            SqlDataReader dr = cmd.ExecuteReader();
        }

        Filme IRepositorioFilme.RetornaFilme(int id)
        {
            throw new NotImplementedException();
        }
    }
}
