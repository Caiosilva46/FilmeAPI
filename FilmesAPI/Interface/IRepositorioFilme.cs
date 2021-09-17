using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    interface IRepositorioFilme
    {
        List<Filme> GetFilme();

        Filme GetFilmeById(int id);

        void PostFilme(Filme filme);

        void PutFilme(Filme filme);

        void DeleteFilme(int id);

        bool GetId(int id);

        bool GetTitulo(Filme filme);
    }
}
