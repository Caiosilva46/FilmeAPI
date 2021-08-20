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
    public class ClienteController : ControllerBase
    {
        private static List<Cliente> clientes = new List<Cliente>();

        [HttpPost]
        public void adicionaFilme(Cliente cliente)
        {
            clientes.Add(cliente);
        }

        [HttpGet]

        public IEnumerable<Cliente> RecuperaCliente()
        {
            return clientes;
        }

    }
}
