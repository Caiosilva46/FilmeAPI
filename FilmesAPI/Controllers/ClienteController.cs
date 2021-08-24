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
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {

        public readonly IServiceCliente serviCli;

        public ClienteController(IServiceCliente service)
        {
            serviCli = service;
        }

        [HttpGet]
        [Route("recuperacliente")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<Cliente> RecuperaCliente()
        {
            return serviCli.RetornaCliente();
        }

        [HttpGet("{id}")]
        [Route("recuperaclienteid")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Cliente RecuperaClienteId(Guid id)
        {
            return serviCli.RetornaClienteId(id);
        }

        [HttpPost]
        [Route("adicionacliente")]
        //[ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public void AdicionaCliente(Cliente cliente)
        {
             serviCli.InsereCliente(cliente); 
        }

        [HttpDelete("{id}")]
        [Route("deletacliente")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public void DeletaCliente (Guid id)
        {
             serviCli.RemoveCliente(id);       
        }

        [HttpPut("{id}")]
        [Route("atualizacliente")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public void AtualizaCliente (Cliente cliente)
        {
             serviCli.AlteraCliente(cliente);
        }
    }
}
