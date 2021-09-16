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

        public List<Locacao> RetornaLocacao()
        {
            return rep.RetornaLocacao();
        }

        public Locacao RetornaLocacaoId(int id)
        {
            return rep.RetornaLocacaoId(id);
        }

        public void AdicionaLocacao(Locacao locacao)
        {
            rep.AdicionaLocacao(locacao);
        }

        public void AtualizaLocacao(Locacao locacao)
        {
            rep.AtualizaLocacao(locacao);
        }

        public void RemoveLocacao(int id)
        {
            rep.RemoveLocacao(id);
        }

        public bool LocalizaId(int id)
        {
            return rep.LocalizaId(id);
        }
    }
}
