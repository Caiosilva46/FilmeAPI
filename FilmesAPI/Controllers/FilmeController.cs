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
    public class FilmeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes()
        {
            return null;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Filme), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AdicionaFilme(Filme filme)
        {
            if(filme != null)
            {
                return CreatedAtAction("AdicionaFilme", filme);
            }

            return BadRequest();
        }
        
        [HttpDelete("{id}")]

        public ActionResult DeletaFilmes (Guid id)
        {
            if (id == null)
            {
                return Ok("Filme excluido com sucesso !");
            }

            return NotFound();
        }


        [HttpPut("{id}")]

        public ActionResult AtualizaFilme (Guid id, Filme filme)
        {
            if (filme != null)
            {


                return Ok();
            }

            return NotFound("O filme não pode ser atualizado !");
        }
    }
}
