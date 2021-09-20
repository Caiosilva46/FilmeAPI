using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmesAPI.Interface;
using FilmesAPI.Models;
using FilmesAPI.Repositorio;

namespace FilmesAPI.Services
{
    public class ServiceLocacao : IServiceLocacao
    {
        private readonly RepositorioLocacao rep = new RepositorioLocacao();

        public List<Locacao> GetLocacao()
        {
            return rep.GetLocacao();
        }

        public Locacao GetLocacaoById(int id)
        {
            return rep.GetLocacaoById(id);
        }

        public void PostLocacao(Locacao locacao)
        {
            rep.PostLocacao(locacao);
        }

        public void PutLocacao(Locacao locacao)
        {
            rep.PutLocacao(locacao);
        }

        public void DeleteLocacao(int id)
        {
            rep.DeleteLocacao(id);
        }

        public bool GetId(int id)
        {
            return rep.GetId(id);
        }

        public bool GetClienteAtivo(int id)
        {
            return rep.GetClienteAtivo(id);
        }
    }
}
