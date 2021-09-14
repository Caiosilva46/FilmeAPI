using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    interface IRepositorioFilme
    {
        //GET
        List<Filme> RetornaFilme();

        //GETID
        Filme RetornaFilmeId(int id);

        //POST
        void AdicionaFilme(Filme filme);

        //PUT
        void AtualizaFilme(Filme filme);

        //DELETE
        void RemoveFilme(int id);

        //GET
        bool LocalizaId(int id);

        //GET
        bool TituloCadastrado(Filme filme);
    }
}
