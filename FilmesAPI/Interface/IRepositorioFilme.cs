using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    interface IRepositorioFilme
    {
        void AlteraFilme(Filme filme);

        void InsereFilme(Filme filme);

        void RemoveFilme(Guid id);

        List<Filme> RetornaFilme();

        Filme RetornaFilmeId(Guid id);
    }
}
