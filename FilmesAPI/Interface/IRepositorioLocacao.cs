using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    interface IRepositorioLocacao
    {
        //PUT
        void AtualizaLocacao(Locacao locacao);

        //POST
        void AdicionaLocacao(Locacao locacao);

        //DELETE
        void RemoveLocacao(int id);

        //GET
        List<Locacao> RetornaLocacao();

        //GETID
        Locacao RetornaLocacaoId(int id);
    }
}
