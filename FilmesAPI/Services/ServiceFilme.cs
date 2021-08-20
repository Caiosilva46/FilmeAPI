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
        public void AlteraFilme(Filme filme)
        {
            rep.AlteraFilme(filme);
        }

        public void InsereFilme(Filme filme)
        {
            rep.InsereFilme(filme);
        }

        public void RemoveFilme(int id)
        {
            rep.RemoveFilme(id);
        }

        public Filme RetornaFilme(int id)
        {
            throw new NotImplementedException();
        }
    }
}
