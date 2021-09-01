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
    [Route("api/filme")]

    public class FilmeController : ControllerBase
    {
        public readonly ServiceFilme _serviceFilme = new ServiceFilme();

        [HttpGet("recuperafilme")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Filme> RecuperaFilme()
        {
            return _serviceFilme.RetornaFilme();
        }

        [HttpGet("recuperafilmeid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Filme> RecuperaFilmeId(int id)
        {
            if(_serviceFilme.LocalizaId(id) != true)
            {
                return BadRequest("Filme não localizado !");
            }

            return _serviceFilme.RetornaFilmeId(id);
        }

        [HttpPost("adicionafilme")]
        [ProducesResponseType(typeof(Filme), StatusCodes.Status201Created)]
        public ActionResult AdicionaFilme(Filme filme)
        {
            _serviceFilme.AdicionaFilme(filme);
            return CreatedAtAction("adicionafilme", filme);
        }
        
        [HttpDelete("deletafilme/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult DeletaFilme (int id)
        {
            if(_serviceFilme.LocalizaId(id) != true)
            {
                return BadRequest("Cliente não localizado !");
            }

            _serviceFilme.RemoveFilme(id);

            return Ok("Item deletado com sucesso!");
        }

        [HttpPut("atualizafilme/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult AtualizaFilme (Filme filme)
        {
            _serviceFilme.AtualizaFilme(filme);
            return Ok("Filme atualizado com sucesso !");
        }
    }
}

