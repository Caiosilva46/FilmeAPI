using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    public interface IServiceFilme
    {
        void AlteraFilme(Filme filme);

        void InsereFilme(Filme filme);

        void RemoveFilme(int id);

        Filme RetornaFilme(int id);
    }
}
