using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Models
{
    public class VincProduto
    {
        
        public Guid Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int Filmeid { get; set; }
        public Filme Filme { get; set; }

        public string Titulo { get; set; }

        public DateTime DataLocacao { get; set; }

        public VincProduto()
        {
            Id = Guid.NewGuid();
        }
    }
}
