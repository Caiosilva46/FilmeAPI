using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    interface IRepositorioLocacao
    {
        //GET
        List<Locacao> RetornaLocacao();

        //GETID
        Locacao RetornaLocacaoId(int id);

        //POST
        void AdicionaLocacao(Locacao locacao);

        //PUT
        void AtualizaLocacao(Locacao locacao);

        //DELETE
        void RemoveLocacao(int id);


        //GETINFO
        bool LocalizaId(int id);

        //GETINFO
        bool
    }
}
