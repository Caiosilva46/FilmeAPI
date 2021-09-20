using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    interface IServiceLocacao
    {

        List<Locacao> GetLocacao();

        Locacao GetLocacaoById(int id);

        void PostLocacao(Locacao locacao);

        void PutLocacao(Locacao locacao);

        void DeleteLocacao(int id);

        bool GetId(int id);

        bool GetClienteAtivo(int id);

    }
}
