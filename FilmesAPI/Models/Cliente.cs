
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using FilmesAPI.Models.ValueObject;

namespace FilmesAPI.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = "O nome não pode conter mais que 200 caracteres ou menos que 1 caracteres")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,200}$", ErrorMessage = "O nome não pode conter números ou caractéres especiais")]
        public string Nome { get; set; }

        public object Cpf { get; set; }

        public object Rg { get; set; }

        public object Email { get; set; }

        [StringLength(15, MinimumLength = 4, ErrorMessage = "A senha teve ter no mínimo 4 digitos e maximo de 15 digitos")]
        public string Senha { get; set; }

        public ICollection<Locacao> Locacao { get; set; }
    }
}
