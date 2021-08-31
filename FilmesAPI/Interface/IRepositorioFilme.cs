using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    interface IRepositorioFilme
    {
        //PUT
        void AtualizaFilme(Filme filme);

        //POST
        void AdicionaFilme(Filme filme);

        //DELETE
        void RemoveFilme(int id);

        //GET
        List<Filme> RetornaFilme();

        //GETID
        Filme RetornaFilmeId(int id);
        
        //GETINFO
        bool LocalizaId(int id);
    }
}
