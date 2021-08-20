using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    public interface IServiceCliente
    {
        void AlteraCliente(Cliente cliente);

        void InsereCliente(Cliente cliente);

        void RemoveCliente(int id);

        Cliente RetornaCliente(int id);
    }
}
