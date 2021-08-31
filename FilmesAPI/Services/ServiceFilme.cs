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
        public void AtualizaFilme(Filme filme)
        {
            rep.AtualizaFilme(filme);
        }

        public void AdicionaFilme(Filme filme)
        {
            rep.AdicionaFilme(filme);
        }

        public void RemoveFilme(int id)
        {
           rep.RemoveFilme(id);
        }

        public List<Filme> RetornaFilme()
        {
            return rep.RetornaFilme();
        }

        public Filme RetornaFilmeId(int id)
        {
            return rep.RetornaFilmeId(id);
        }

        public bool LocalizaId(int id)
        {
            return rep.LocalizaId(id);
        }
    }
}
