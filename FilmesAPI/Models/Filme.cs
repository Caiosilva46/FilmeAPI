using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 2, ErrorMessage = "O Título deve conter no máximo 30 caracteres e minimo de 2 caracteres.")]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,30}$", ErrorMessage = "O título não pode conter caractéres especiais")]
        public string Titulo { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "O Gênero deve conter no máximo 20 caracteres e no mínimo 3 caracteres.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "O Gênero não pode conter números ou caracteres especiais")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "A data é obrigatória para a locação do produto")]
        [DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; }

        public ICollection<Locacao> Locacao { get; set; }

    }
}
