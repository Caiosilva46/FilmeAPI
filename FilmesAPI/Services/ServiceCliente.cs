using FilmesAPI.Interface;
using FilmesAPI.Models;
using FilmesAPI.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class ServiceCliente : IServiceCliente
    {
        private readonly RepositorioCliente rep = new RepositorioCliente(); 
        public void AlteraCliente(Cliente cliente)
        {
            rep.AlteraCliente(cliente);
        }

        public void InsereCliente(Cliente cliente)
        {
            rep.InsereCliente(cliente);
        }

        public void RemoveCliente(Guid id)
        {
            rep.RemoverCliente(id);
        }

        public Cliente RetornaClienteId(Guid id)
        {
             return rep.RetornaClienteId(id);
        }

        public List<Cliente> RetornaCliente()
        {
            return rep.RetornaCliente();
        }
    }
}
