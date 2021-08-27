using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using FilmesAPI.Repositorio;
using FilmesAPI.Interface;
using FilmesAPI.Services;

namespace FilmesAPI.Controllers
{

    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {

        public readonly ServiceCliente _serviceCli = new ServiceCliente();

        [HttpGet("recuperacliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Cliente> RecuperaCliente()
        {
            return _serviceCli.RetornaCliente();
        }

        [HttpGet("recuperaclienteid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Cliente RecuperaClienteId(int id)
        {
            return _serviceCli.RetornaClienteId(id);            
        }

        [HttpPost("adicionacliente")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AdicionaCliente(Cliente cliente)
        {

            if(_serviceCli.CpfCadastrado(cliente.Cpf))
            
                throw new Exception("CPF Já cadastrao para outro cliente !");
            
            _serviceCli.AdicionaCliente(cliente);
            return CreatedAtAction("", cliente);

        }

        [HttpDelete("deletacliente/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeletaCliente (int id)
        {
            _serviceCli.RemoveCliente(id);
            return Ok("Cliente excluido com sucesso !");
        }

        [HttpPut("atualizacliente/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult AtualizaCliente (Cliente cliente)
        {
            _serviceCli.AtualizaCliente(cliente);
            return Ok("Cliente atualizado com sucesso !");
        }

    }
}
