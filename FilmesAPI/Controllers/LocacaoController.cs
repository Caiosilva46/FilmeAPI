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
        public ActionResult<Locacao> GetLocacaoById(int id)
        {
            if (!_serviceLocacao.GetId(id))
            {
                return BadRequest("Locação não localizada!");
            }
            return _serviceLocacao.GetLocacaoById(id);
        }

        [HttpGet("recuperalocacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Locacao> GetLocacao()
        {
            return _serviceLocacao.GetLocacao();
        }

        [HttpPost("adicionalocacao")]
        [ProducesResponseType(typeof(Locacao), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PostLocacao(Locacao locacao)
        {
            _serviceLocacao.PostLocacao(locacao);
            return CreatedAtAction("adicionalocacao", locacao);
        }

        [HttpPut("atualizalocacao/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PutLocacao(Locacao locacao)
        {
            if (!_serviceLocacao.GetId(locacao.Id))
            {
                return BadRequest("Locação não localizada para atualização!");
            }
            _serviceLocacao.PutLocacao(locacao);
            return Ok("Locação atualizada com sucesso !");
        }

        [HttpDelete("deletalocacao/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteLocacao(int id)
        {
            if (!_serviceLocacao.GetId(id))
            {
                return BadRequest("Locação não localizada !");
            }
            _serviceLocacao.DeleteLocacao(id);
            return Ok("Locação excluida com sucesso !");
        }
    }
}
