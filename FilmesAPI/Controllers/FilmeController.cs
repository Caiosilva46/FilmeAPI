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
        public readonly IServiceFilme serviFilme;

        public FilmeController(IServiceFilme service)
        {
            serviFilme = service;
        }

        [HttpGet]
        [Route("recuperafilme")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Filme> RecuperaFilme()
        {
            return serviFilme.RetornaFilme();
        }

        [HttpGet("{id}")]
        [Route("recuperafilme")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public Filme RecuperaFilme(Guid id)
        {
            return serviFilme.RetornaFilmeId(id);
        }

        [HttpPost]
        [Route("adicionafilme")]
        //[ProducesResponseType(typeof(Filme), StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public void AdicionaFilme(Filme filme)
        {
            serviFilme.InsereFilme(filme);
        }
        
        [HttpDelete("{id}")]
        [Route("deletafilme")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        public void DeletaFilme (Guid id)
        {
            serviFilme.RemoveFilme(id);
        }

        [HttpPut("{id}")]
        [Route("atualizafilme")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        public void AtualizaFilme (Filme filme)
        {
            serviFilme.AlteraFilme(filme);
        }
    }
}

