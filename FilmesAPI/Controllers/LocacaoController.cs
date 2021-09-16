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
    [Route("api/locacao")]
    public class LocacaoController : ControllerBase
    {
        public readonly ServiceLocacao _serviceLocacao = new ServiceLocacao();

        [HttpGet("recuperalocacaoid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Locacao> RetornaLocacaoId(int id)
        {
            if (!_serviceLocacao.LocalizaId(id))
            {
                return BadRequest("Locação não localizada!");
            }
            return _serviceLocacao.RetornaLocacaoId(id);
        }

        [HttpGet("recuperalocacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Locacao> RecuperaLocacao()
        {
            return _serviceLocacao.RetornaLocacao();
        }

        [HttpPost("adicionalocacao")]
        [ProducesResponseType(typeof(Locacao), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AdicionaLocacao(Locacao locacao)
        {
            _serviceLocacao.AdicionaLocacao(locacao);
            return CreatedAtAction("adicionalocacao", locacao);

            return Ok("Locação efetuada com sucesso !");
        }

        [HttpDelete("deletalocacao/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeletaLocacao(int id)
        {
            if (!_serviceLocacao.LocalizaId(id))
            {
                return BadRequest("Locação não localizada !");
            }
            _serviceLocacao.RemoveLocacao(id);
            return Ok("Locação excluida com sucesso !");
        }

        [HttpPut("atualizalocacao/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AtualizaLocacao(Locacao locacao)
        {
            if (!_serviceLocacao.LocalizaId(locacao.Id))
            {
                return BadRequest("Locação não localizada para atualização!");
            }
            _serviceLocacao.AtualizaLocacao(locacao);
            return Ok("Locação atualizada com sucesso !");
        }

    }
}
