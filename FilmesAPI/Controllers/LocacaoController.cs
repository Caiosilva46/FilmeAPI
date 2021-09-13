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

        [HttpGet("recuperalocacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Locacao> RecuperaLocacao()
        {
            return _serviceLocacao.RetornaLocacao();
        }

        [HttpGet("recuperalocacaoid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Locacao RetornaLocacaoId(int id)
        {
            return _serviceLocacao.RetornaLocacaoId(id);            
        }

        [HttpPost("adicionalocacao")]
        [ProducesResponseType(typeof(Locacao), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AdicionaLocacao(Locacao locacao)
        {

            _serviceLocacao.AdicionaLocacao(locacao);
            return CreatedAtAction("adicionalocacao", locacao);

        }

        [HttpDelete("deletalocacao/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeletaLocacao (int id)
        {
            _serviceLocacao.RemoveLocacao(id);
            return Ok("Locação excluida com sucesso !");
        }

        [HttpPut("atualizalocacao/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult AtualizaLocacao (Locacao locacao)
        {
            _serviceLocacao.AtualizaLocacao(locacao);
            return Ok("Locação atualizada com sucesso !");
        }

    }
}
