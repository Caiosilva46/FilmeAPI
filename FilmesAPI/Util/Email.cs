using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FilmesAPI.Models.ValueObject
{
    public class Email
    {
        public Email()
        {
            
        }

        public string ValidaEmail (string email)
        {
            try
            {
                if (!ValidarEmail(email))

                    throw new Exception();
            }
            catch (Exception)
            {

                throw new Exception("Email informado é inválido: " + email);
            }

            return email;
        }

        //Metodo de validar Email com REGEX
        public bool ValidarEmail(string email)
        {
            bool emailValido = false;

            //Expressão regular Regex para validação de email.
            string emailRegex = string.Format("{0}{1}",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            try
            {
                emailValido = Regex.IsMatch(
                    email,
                    emailRegex);
            }
            catch (RegexMatchTimeoutException)
            {
                emailValido = false;
            }

            return emailValido;
        }
    }
}

