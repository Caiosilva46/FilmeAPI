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
        public IEnumerable<Filme> GetFilme()
        {
            return _serviceFilme.GetFilme();
        }

        [HttpGet("recuperafilmeid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Filme> GetFilmeById(int id)
        {
            if (!_serviceFilme.GetId(id))
            {
                return BadRequest("Filme não localizado!");
            }

            return _serviceFilme.GetFilmeById(id);
        }

        [HttpPost("adicionafilme")]
        [ProducesResponseType(typeof(Filme), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AdicionaFilme(Filme filme)
        {
            if (_serviceFilme.GetTitulo(filme))
            {
                return BadRequest("Já existe um filme cadastrado com esse Título e Gênero!");
            }
            _serviceFilme.PostFilme(filme);
            return CreatedAtAction("adicionafilme", filme);
        }

        [HttpPut("atualizafilme/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AtualizaFilme(Filme filme)
        {
            if (!_serviceFilme.GetId(filme.Id))
            {
                return BadRequest("Filme não localizado para atualização!");
            }
            else if (_serviceFilme.GetTitulo(filme))
            {
                return BadRequest("Já existe um filme cadastrado com esse Título e Gênero!");
            }
            _serviceFilme.PutFilme(filme);
            return Ok("Filme atualizado com sucesso!");
        }

        [HttpDelete("deletafilme/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeletaFilme(int id)
        {
            if (_serviceFilme.GetStatusLocacao(id) == true)
            {
                return BadRequest("Filme não pode ser excluido, pois esta locado para um cliente!");
            }
            if (!_serviceFilme.GetId(id))
            {
                return BadRequest("Filme não localizado!");
            }
            _serviceFilme.DeleteFilme(id);

            return Ok("Item deletado com sucesso!");
        }
    }
}

