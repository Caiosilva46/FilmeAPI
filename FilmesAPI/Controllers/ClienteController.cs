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

namespace FilmesAPI.Controllers
{

    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("recuperacliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Cliente>> RecuperaCliente()
        {
            var clientes = new Cliente[] { };

            if (clientes == null)
            {
                return BadRequest("Não há clientes cadastrados !");
            }

            return Ok(clientes);
        }

        [HttpPost]
        [Route("adicionacliente")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AdicionaCliente(Cliente cliente)
        {
            if (cliente != null)
            {
                return CreatedAtAction("AdicionaCliente", cliente);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Route("deletacliente")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeletaCliente (Guid id)
        {
            if (id != null)
            {
                return Ok("Cliente excluido com sucesso ! ");
            }

            return BadRequest("Cliente não localizado !");
        }

        [HttpPut("{id}")]
        [Route("atualizacliente")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult AtualizaCliente (Guid id, Cliente cliente)
        {
            if (cliente != null)
            {
                return Ok("Cliente atualizado com sucesso !");
            }

            return BadRequest("Cliente não pode ser atualizado");
        }
    }
}
