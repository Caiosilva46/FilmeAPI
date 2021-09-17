using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    interface IServiceLocacao
    {

        //GET
        List<Locacao> GetLocacao();

        //GETID
        Locacao GetLocacaoById(int id);

        //POST
        void PostLocacao(Locacao locacao);

        //PUT
        void PutLocacao(Locacao locacao);

        //DELETE
        void DeleteLocacao(int id);

        //GETINFO
        bool GetId(int id);

    }
}
