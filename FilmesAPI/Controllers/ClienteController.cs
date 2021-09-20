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
        public ActionResult<Cliente> GetClienteById(int id)
        {
            if (!_serviceCliente.GetId(id))
            {
                return BadRequest("Cliente não encontrado !");
            }
            return _serviceCliente.GetClienteById(id);
        }

        [HttpGet("recuperacliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Cliente> GetCliente()
        {
            return _serviceCliente.GetCliente();
        }

        [HttpPost("adicionacliente")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PostCliente(Cliente cliente)
        {
            _serviceCliente.ValidaCliente(cliente);

            if (_serviceCliente.GetCpf(cliente.Cpf.ToString()))
            {
                return BadRequest("CPF Já cadastrao para outro cliente !");
            }
            else if (_serviceCliente.GetEmail(cliente.Email.ToString()))
            {
                return BadRequest("Email Já cadastrado para outro cliente!");
            }

            _serviceCliente.PostCliente(cliente);

            return CreatedAtAction("adicionacliente", cliente);
        }

        [HttpPut("atualizacliente/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PutCliente(Cliente cliente)
        {
            if (!_serviceCliente.GetId(cliente.Id))
            {
                return BadRequest("Cliente não localizado !");

            }
            else if (_serviceCliente.GetEmail(cliente.Email.ToString()) == true)
            {
                return BadRequest("Email já cadastrado para outro cliente, por favor, utilizar outro e-mail !");
            }
            _serviceCliente.PutCliente(cliente);

            return Ok("Cliente atualizado com sucesso !");
        }

        [HttpDelete("deletacliente/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteCliente(int id)
        {
            if (_serviceCliente.GetStatusLocacao(id) == true)
            {
                return BadRequest("Cliente não pode ser excluido, filmes locados em seu nome!");
            }

            if (!_serviceCliente.GetId(id))
            {
                return BadRequest("Cliente não localizado !");
            }

            _serviceCliente.DeleteCliente(id);

            return Ok("Cliente excluido com sucesso !");
        }

        [HttpPost("logincliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PostLoginCliente(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.Email.ToString()) && string.IsNullOrEmpty(cliente.Senha))
            {
                return BadRequest("Email e senha precisam ser informados!");
            }

            if (!_serviceCliente.GetEmail(cliente.Email.ToString()))
            {
                return BadRequest("Email inválido!");
            }

            var senhaHash = _serviceCliente.CrypSenha(cliente.Senha);

            if (!_serviceCliente.GetSenha(senhaHash, cliente.Email.ToString()))
            {
                return BadRequest("Senha inválido!");
            }
            return Ok("Cliente logado com sucesso !");
        }
    }
}