
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
        public int ClienteId { get; set; }

        [Required(ErrorMessage ="O nome do cliente é obrigatório")]
        [StringLength(200, MinimumLength = 1,ErrorMessage ="O nome não pode conter mais que 200 caracteres ou menos que 1 caracteres")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,200}$",ErrorMessage ="O nome não pode conter números ou caractéres especiais")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="O CPF é obrigatório")]
        public CPF Cpf { get; set; }

        [Required(ErrorMessage ="O RG é obrigatório")]
        [StringLength(12,ErrorMessage ="O RG Não pode conter mais que 12 dígitos")]
        [RegularExpression(@"(^[0-9]{2}[.][0-9]{3}[.][0-9]{3}[-][a-zA-Z0-9]{1})$", ErrorMessage ="Esse RG não é válido! Por favor, informe um RG Válido")]
        public string RG { get; set; }

        [Required(ErrorMessage ="O Email é obrigatório")]
        public Email email { get; set; }

        [Required(ErrorMessage ="A senha do cliente é obrigatório")]
        [StringLength(15, MinimumLength =4, ErrorMessage ="A senha teve ter no mínimo 4 digitos e maximo de 15 digitos")]
        public string Senha { get; set; }


    }
}
