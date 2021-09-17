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
        //GET
        List<Cliente> GetCliente();

        //GETID
        Cliente GetClienteById(int id);

        //POST
        void PostCliente(Cliente cliente);

        //PUT
        void PutCliente(Cliente cliente);

        //DELETE
        void DeleteCliente(int id);

        //GETINFO
        bool GetId(int id);

        //GET 
        bool GetCpf(string cpf);

        //GET
        bool GetEmail(string email);

        //GET
        bool GetSenha(string senha, string email);
    }
}
