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

        void RemoveCliente(Guid id);

        List<Cliente> RetornaCliente();

        Cliente RetornaClienteId(Guid id);
    }
}
