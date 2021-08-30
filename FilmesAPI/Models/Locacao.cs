using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Models
{
    public class Locacao
    {
        [Key]
        public int LocacaoId { get; set; }

        [Required(ErrorMessage = "O campo título é obrigatório")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "O Título deve conter no máximo 30 caracteres e minimo de 2 caracteres.")]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,30}$", ErrorMessage = "O título não pode conter caractéres especiais")]
        public string Titulo { get; set; }

        [Required(ErrorMessage ="O valor deve ser informado !")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$")]
        [Range(0, 9999.99, ErrorMessage ="O valor não pode ultrapassar 10 mil reais !")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "A data é obrigatória para a locação do produto")]
        [DataType(DataType.Date)]
        public DateTime DataRetirada { get; set; }

        public DateTime DataDevolucao { get; set; }

        public int FilmeId { get; set; }

        public int ClienteId { get; set; }



    }
}
