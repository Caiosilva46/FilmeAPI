using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace FilmesAPI.Models
{
    public class Cliente
    {
        
        public Guid Id { get; set; }

        [Required(ErrorMessage ="O nome do cliente é obrigatório")]
        [StringLength(200, MinimumLength =3,ErrorMessage ="O nome não pode conter mais que 200 caracteres ou menos que 3 caracteres")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,200}$",ErrorMessage ="O nome não pode conter números ou caractéres especiais")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="O CPF é obrigatório")]
        [StringLength(14,ErrorMessage ="O CPF não pode conter mais que 14 dígitos")]
        [RegularExpression(@"(^[0-9]{3}[.][0-9]{3}[.][0-9]{3}[-][0-9]{2})$", ErrorMessage = "Esse CPF não é válido! Por favor, informe um CPF Válido")]
        public string CPF { get; set; }

        [Required(ErrorMessage ="O RG é obrigatório")]
        [StringLength(12,ErrorMessage ="O RG Não pode conter mais que 12 dígitos")]
        [RegularExpression(@"(^[0-9]{2}[.][0-9]{3}[.][0-9]{3}[-][a-zA-Z0-9]{1})$", ErrorMessage ="Esse RG não é válido! Por favor, informe um RG Válido")]
        public string RG { get; set; }

        [Required(ErrorMessage ="O Email é obrigatório")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="A senha do cliente é obrigatório")]
        [StringLength(15, MinimumLength =4, ErrorMessage ="A senha teve ter no mínimo 4 digitos e maximo de 15 digitos")]
        public string Senha { get; set; }

        public Cliente()
        {
            Id = Guid.NewGuid();      
        }

        private string HashMd5 (MD5 md5Hash, string senha)
        {
            byte[] senhaMd = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < senhaMd.Length; i++)
            {
                sBuilder.Append(senhaMd[i].ToString("X2"));
            }

            return sBuilder.ToString();
        }

    }
}
