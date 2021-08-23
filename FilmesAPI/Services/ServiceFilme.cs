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

        public void RemoveFilme(Guid id)
        {
           rep.RemoveFilme(id);
        }

        public List<Filme> RetornaFilme()
        {
            return rep.RetornaFilme();
        }

        public Filme RetornaFilmeId(Guid id)
        {
            return rep.RetornaFilmeId(id);
        }
    }
}
