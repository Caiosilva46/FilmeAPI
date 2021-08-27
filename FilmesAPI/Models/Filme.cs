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
        public int FilmeId { get; set; }

        [Required(ErrorMessage ="O campo título é obrigatório")]
        [StringLength(30, MinimumLength =2, ErrorMessage ="O Título deve conter no máximo 30 caracteres e minimo de 2 caracteres.")]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,30}$", ErrorMessage = "O título não pode conter caractéres especiais")]
        public string Titulo { get; set; }

        [Required(ErrorMessage ="O campo gêmero é obrigatório")]
        [StringLength(20,MinimumLength =3, ErrorMessage = "O Gênero deve conter no máximo 20 caracteres e no mínimo 3 caracteres.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "O Gênero não pode conter números ou caracteres especiais")]
        public string Genero { get; set; }

        [Required]
        [DataType(DataType.Date,ErrorMessage ="A data é obrigatória para o cadastro do produto")]
        [StringLength(10, MinimumLength = 10, ErrorMessage ="A data precisa ter 6 caracteres!")]
        [RegularExpression(@"^[0-9]{2}[/][0-9]{2}[/][0-9]{4}", ErrorMessage ="Inserir a data no padrão dd/mm/yy")]
        public string DataCadastro { get; set; }

    }
}
