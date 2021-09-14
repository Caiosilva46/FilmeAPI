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
using FilmesAPI.Models.ValueObject;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        public readonly ServiceCliente _serviceCliente = new ServiceCliente();

        [HttpGet("recuperaclienteid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Cliente> RecuperaClienteId(int id)
        {
            if (!_serviceCliente.LocalizaId(id) || id <= 0)
            {
                return BadRequest("Cliente não encontrado !");
            }
            return _serviceCliente.RetornaClienteId(id);
        }

        [HttpGet("recuperacliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Cliente> RecuperaCliente()
        {
            return _serviceCliente.RetornaCliente();
        }

        [HttpPost("adicionacliente")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AdicionaCliente(Cliente cliente)
        {
            _serviceCliente.ValidaCliente(cliente);

            if (_serviceCliente.CpfCadastrado(cliente.Cpf.ToString()))
            {
                return BadRequest("CPF Já cadastrao para outro cliente !");
            }
            else if (_serviceCliente.EmailCadastrado(cliente.Email.ToString()))
            {
                return BadRequest("Email Já cadastrao para outro cliente !");
            }

            _serviceCliente.AdicionaCliente(cliente);

            return CreatedAtAction("adicionacliente", cliente);
        }

        [HttpPut("atualizacliente/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AtualizaCliente(Cliente cliente)
        {
            if(!_serviceCliente.LocalizaId(cliente.Id))
            {
                return BadRequest("Cliente não localizado !");
            }
            _serviceCliente.AtualizaCliente(cliente);

            return Ok("Cliente atualizado com sucesso !");
        }

        [HttpDelete("deletacliente/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeletaCliente(int id)
        {
            if (!_serviceCliente.LocalizaId(id) || id <= 0)
            {
                return BadRequest("Cliente não localizado !");
            }

            _serviceCliente.RemoveCliente(id);

            return Ok("Cliente excluido com sucesso !");
        }

        [HttpPost("logincliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult LoginCliente(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.Email.ToString()) && string.IsNullOrEmpty(cliente.Senha))
            {
                return BadRequest("Email e senha precisam ser informados!");
            }

            if (!_serviceCliente.EmailCadastrado(cliente.Email.ToString()))
            {
                return BadRequest("Email inválido!");
            }

            var senhaHash = _serviceCliente.CrypSenha(cliente.Senha);

            if (!_serviceCliente.SenhaCadastrada(senhaHash))
            {
                return BadRequest("Senha inválido!");
            }
            return Ok("Cliente logado com sucesso !");
        }
    }
}