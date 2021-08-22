using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace FilmesAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {


        [HttpGet]
        public IEnumerable<Cliente> RecuperaCliente()
        {
            return null;
        }

        [HttpPost]
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

        public ActionResult DeletaCliente (Guid id)
        {
            if (id != null)
            {
                return Ok("Cliente excluido com sucesso ! ");
            }

            return BadRequest("Cliente não localizado !");
        }

        [HttpPut("{id}")]

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
