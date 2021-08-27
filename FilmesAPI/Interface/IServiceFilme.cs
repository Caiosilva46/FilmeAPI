using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    public interface IServiceFilme
    {
        void AtualizaFilme(Filme filme);

        void AdicionaFilme(Filme filme);

        void RemoveFilme(int id);

        List<Filme> RetornaFilme();

        Filme RetornaFilmeId(int id);
    }
}
