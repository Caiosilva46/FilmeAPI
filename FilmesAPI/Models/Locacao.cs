using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Models
{
    public class Locacao
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 9999.99, ErrorMessage = "O valor não pode ser negativo ou ultrapassar 10 mil reais !")]
        public decimal Valor { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataRetirada { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataDevolucao { get; set; }

        public int FilmeId { get; set; }

        public int ClienteId { get; set; }

        public bool Ativo { get; set; }

        [Range(1, 100, ErrorMessage = "É preciso informar a quantidade locada!")]
        public int QtdLocado { get; set; }

        public Cliente Cliente { get; set; }

        public Filme Filme { get; set; }
    }
}
