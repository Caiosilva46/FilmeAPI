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
        public ActionResult<Cliente> RecuperaClienteId(int id)
        {

            if(_serviceCliente.LocalizaId(id) != true)
            {
                return BadRequest("Cliente não encontrado !");
            }

            return _serviceCliente.RetornaClienteId(id);

        }

        [HttpPost("adicionacliente")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AdicionaCliente(Cliente cliente)
        {

            _serviceCliente.CpfCadastrado(cliente.Cpf);

            //throw new Exception("CPF Já cadastrao para outro cliente !");

            _serviceCliente.AdicionaCliente(cliente);
            return CreatedAtAction("adicionacliente", cliente);

        }

        [HttpDelete("deletacliente/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeletaCliente (int id)
        {
            if(_serviceCliente.LocalizaId(id) != true)
            {
                return BadRequest("Cliente não localizado !");
            }

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

        public ActionResult LoginCliente(string email, string senha)
        {
            _serviceCliente.LoginCliente(senha,email);
            //_serviceCliente.VerificarHash(senha);
            return Ok("Cliente logado com sucesso !");

        }

    }
}
