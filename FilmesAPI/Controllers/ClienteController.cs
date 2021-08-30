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

        public readonly ServiceCliente _serviceCliente = new ServiceCliente();

        [HttpGet("recuperacliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Cliente> RecuperaCliente()
        {
            return _serviceCliente.RetornaCliente();
        }

        [HttpGet("recuperaclienteid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Cliente RecuperaClienteId(int id)
        {
            return _serviceCliente.RetornaClienteId(id);            
        }

        [HttpPost("adicionacliente")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AdicionaCliente(Cliente cliente)
        {

           // if(_serviceCli.CpfCadastrado(cliente.Cpf))
            
                //throw new Exception("CPF Já cadastrao para outro cliente !");
            
            _serviceCliente.AdicionaCliente(cliente);
            return CreatedAtAction("", cliente);

        }

        [HttpDelete("deletacliente/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeletaCliente (int id)
        {
            _serviceCliente.RemoveCliente(id);
            return Ok("Cliente excluido com sucesso !");
        }

        [HttpPut("atualizacliente/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult AtualizaCliente (Cliente cliente)
        {
            _serviceCliente.AtualizaCliente(cliente);
            return Ok("Cliente atualizado com sucesso !");
        }

        [HttpGet("logincliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult LoginCliente(Cliente cliente)
        {
            _serviceCliente.LoginCliente(cliente);
            //_serviceCli.VerificarHash();
            return Ok("Cliente logado com sucesso !");

        }


    }
}
