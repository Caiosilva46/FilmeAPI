using FilmesAPI.Models;
using FilmesAPI.Models.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Interface
{
    public interface IServiceCliente
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
        bool LoginCliente(string senha, string email);

        //GETINFO
        bool LocalizaId(int id);
    }
}
