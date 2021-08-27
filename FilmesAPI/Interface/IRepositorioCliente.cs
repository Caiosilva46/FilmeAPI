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
        void AtualizaCliente(Cliente cliente);

        void AdicionaCliente(Cliente cliente);

        void RemoveCliente(int id);

        List<Cliente> RetornaCliente();

        Cliente RetornaClienteId(int id);

        void CpfCadastrado(CPF cpf);
    }
}
