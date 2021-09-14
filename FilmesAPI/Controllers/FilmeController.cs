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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Filme> RecuperaFilmeId(int id)
        {
            if (!_serviceFilme.LocalizaId(id) || id <= 0)
            {
                return BadRequest("Filme não localizado !");
            }

            return _serviceFilme.RetornaFilmeId(id);
        }

        [HttpPost("adicionafilme")]
        [ProducesResponseType(typeof(Filme), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AdicionaFilme(Filme filme)
        {
            if (_serviceFilme.TituloCadastrado(filme))
            {
                return BadRequest("Já existe um filme cadastrado com esse Título e Gênero !");
            }
            _serviceFilme.AdicionaFilme(filme);
            return CreatedAtAction("adicionafilme", filme);
        }

        [HttpPut("atualizafilme/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AtualizaFilme(Filme filme)
        {
            if (!_serviceFilme.LocalizaId(filme.Id))
            {
                return BadRequest("");
            }
            _serviceFilme.AtualizaFilme(filme);
            return Ok("Filme atualizado com sucesso !");
        }

        [HttpDelete("deletafilme/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeletaFilme(int id)
        {
            if (!_serviceFilme.LocalizaId(id) || id <= 0)
            {
                return BadRequest("Filme não localizado !");
            }
            _serviceFilme.RemoveFilme(id);

            return Ok("Item deletado com sucesso!");
        }
    }
}

