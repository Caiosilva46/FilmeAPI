using FilmesAPI.Interface;
using FilmesAPI.Models;
using FilmesAPI.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class ServiceFilme : IServiceFilme
    {
        private readonly RepositorioFilme rep = new RepositorioFilme();

        public List<Filme> GetFilme()
        {
            return rep.GetFilme();
        }

        public Filme GetFilmeById(int id)
        {
            return rep.GetFilmeById(id);
        }

        public void PostFilme(Filme filme)
        {
            rep.PostFilme(filme);
        }

        public void PutFilme(Filme filme)
        {
            rep.PutFilme(filme);
        }

        public void DeleteFilme(int id)
        {
            rep.DeleteFilme(id);
        }

        public bool GetId(int id)
        {
            return rep.GetId(id);
        }

        public bool GetTitulo(Filme filme)
        {
            filme.Titulo = filme.Titulo.Trim();
            filme.Genero = filme.Genero.Trim();
            return rep.GetTitulo(filme);
        }

        public bool GetStatusLocacao(int id)
        {
            return rep.GetStatusLocacao(id);
        }
    }
}
