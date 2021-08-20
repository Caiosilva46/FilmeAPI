using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private static List<Filme> filmes = new List<Filme>();

        [HttpPost]
        public void adicionaFilme (Filme filme)
        {
            filmes.Add(filme);
        }

        [HttpGet]

        public IEnumerable<Filme> RecuperaFilmes ()
        {
            return filmes;
        }

    }
}
