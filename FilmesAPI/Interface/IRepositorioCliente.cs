using FilmesAPI.Models;
using FilmesAPI.Models.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    public interface IRepositorioCliente
    {
        //GET
        List<Cliente> RetornaCliente();

        //GETID
        Cliente RetornaClienteId(int id);

        //POST
        void AdicionaCliente(Cliente cliente);

        //PUT
        void AtualizaCliente(Cliente cliente);

        //DELETE
        void RemoveCliente(int id);

        //GET
        bool LocalizaId(int id);

        //GET
        bool CpfCadastrado(string cpf);
       
        //GET
        bool EmailCadastrado(string email);

        //GET
        bool SenhaCadastrada(string senha);
    }
}
