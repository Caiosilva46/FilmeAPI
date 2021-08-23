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
    [Route("api/filme")]
    public class FilmeController : ControllerBase
    {
        [HttpGet]
        [Route("recuperafilme")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Filme>> RecuperaFilme()
        {
            try
            {
                IServiceFilme.AlteraFilme();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("adicionafilme")]
        [ProducesResponseType(typeof(Filme), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AdicionaFilme(Filme filme)
        {

            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            
            
            
            
            
            
            
            /* if(filme != null)
            {
                return CreatedAtAction("AdicionaFilme", filme);
            }

            return BadRequest();*/
        }
        
        [HttpDelete("{id}")]
        [Route("deletafilme")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult DeletaFilme (Guid id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    //ai  vc faz o select pra validar que existe.

                    //o retorno do metodo valida que tem, ai faço o que deleta


                }
                else
                {
                    // alguma mensagem dizendo que ta errado
                }
                // Vou fazer um select para retornar filme por ID usando metodo FilmeID
                //select id from Filme where


            }
            catch (Exception ex)
            {
            return null;
            }
           



            /*if (id == null)
            {
                return Ok("Filme excluido com sucesso !");
            }

            return NotFound();*/

        }

        [HttpPut("{id}")]
        [Route("atualizafilme")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

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

