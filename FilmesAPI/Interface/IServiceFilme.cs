using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    public interface IServiceFilme
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

        //GET
        bool LocalizaId(int id);

        //GET
        bool TituloCadastrado(Filme filme);
    }
}
