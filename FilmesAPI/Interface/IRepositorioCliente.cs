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
        //PUT
        void AtualizaCliente(Cliente cliente);

        //POST
        void AdicionaCliente(Cliente cliente);

        //DELETE
        void RemoveCliente(int id);

        //GET
        List<Cliente> RetornaCliente();

        //GETID
        Cliente RetornaClienteId(int id);

        //GET
        void CpfCadastrado(CPF cpf);

        //GET
        void LoginCliente(Cliente cliente);
       
        //GET
        bool LocalizaId(int id);
    }
}
