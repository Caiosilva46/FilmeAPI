using FilmesAPI.Interface;
using FilmesAPI.Models;
using FilmesAPI.Models.ValueObject;
using FilmesAPI.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class ServiceCliente : IServiceCliente
    {
        private readonly RepositorioCliente rep = new RepositorioCliente();

        public string CpfFinal;
        public string RgFinal;
        public string EmailFinal;

        public Cliente RetornaClienteId(int id)
        {
            return rep.RetornaClienteId(id);
        }

        public List<Cliente> RetornaCliente()
        {
            return rep.RetornaCliente();
        }

        public void AdicionaCliente(Cliente cliente)
        {
            cliente.Cpf = CpfFinal;
            cliente.Email = EmailFinal;
            cliente.Rg = RgFinal; 
            rep.AdicionaCliente(cliente);
        }

        public void AtualizaCliente(Cliente cliente)
        {
            cliente.Senha = CrypSenha(cliente.Senha);

            rep.AtualizaCliente(cliente);
        }

        public void RemoveCliente(int id)
        {
            rep.RemoveCliente(id);
        }

        public bool LocalizaId(int id)
        {
            return rep.LocalizaId(id);
        }

        public bool CpfCadastrado(string Cpf)
        {
            Cpf = CpfFinal;
            return rep.CpfCadastrado(Cpf);
        }

        public bool EmailCadastrado(string email)
        {
            email = EmailFinal;
            return rep.EmailCadastrado(email);
        }

        public bool SenhaCadastrada(string senha)
        {
            return rep.SenhaCadastrada(senha);
        }

        public string CrypSenha(string senha)
        {
            MD5 md5Hash = MD5.Create();
            byte[] senhaMd = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < senhaMd.Length; i++)
            {
                sBuilder.Append(senhaMd[i].ToString("X2"));
            }

            return sBuilder.ToString();
        }

        public void ValidaCliente(Cliente cliente)
        {
            string CpfValido = cliente.Cpf.ToString();
            CpfFinal = ValidaCpf(CpfValido);

            string rgValido = cliente.Rg.ToString();
            RgFinal = ValidaRg(rgValido);

            string emailValido = cliente.Email.ToString();
            EmailFinal = ValidaEmail(emailValido);

            if (!CpfCadastrado(CpfFinal))
            {
                cliente.Senha = CrypSenha(cliente.Senha);
            }
        }

        public string ValidaCpf(string cpf)
        {
            string cpfValido = string.Empty;
            try
            {
                cpfValido = LimpaCPF(cpf);

                cpfValido = ValidarTamanhoCPf(cpfValido);

                //metodo que verificar CPF (cpf)    
                if (!VerificarCPf(cpfValido))
                    throw new Exception();

            }
            catch (Exception ex)
            {
                throw new Exception("O CPF informando não é válido" + ex);
            }

            return cpfValido;
        }

        //TODO criar metodo para limpar CPF
        public string LimpaCPF(string cpf)
        {
            cpf = cpf.Trim(); // Remove todos espaços em branco tanto no inicio quanto no final da strng CPF 
            //cpf = cpf.Replace(".", "").Replace("-", "").Replace("/",""); // remove todos os pontos ou traços e substitui por um valor vazio no lugar;
            cpf = Regex.Replace(cpf, "[^0-9]", "");

            return cpf;
        }

        //Criar um metodo para validar o tamanho do CPF
        public string ValidarTamanhoCPf(string cpf)
        {
            string novocpf = cpf.PadLeft(11, '0'); // PADLEFT adiciona zeros a esquerda da variavel declarada.
            return novocpf; // Criar metodo para validar o CPF se ele tem 11 caracteres, caso tenha menos, adicionar zeros a esquerda
        }

        //Criar metodo para validar o CPF
        public bool VerificarCPf(string cpf)
        {
            int[] Digito1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] Digito2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string CPF1;
            string CPF2;
            int soma = 0;
            int resultado;

            if (cpf.Length != 11) // O CPF não pode passar de 11 digitos numericos.
                return false;

            CPF1 = cpf.Substring(0, 9); // Substring irá contar dez possições apartir do vetor 0


            for (int i = 0; i < 9; i++) // utilizar o FOR para obter o vetor do array CPF inserido e multiplicar pelo vetor Digito1 
            {
                soma += int.Parse(CPF1[i].ToString()) * Digito1[i];
            }

            resultado = soma % 11; // irá receber o resto da divisão

            if (resultado < 2)
            {
                resultado = 0; // Se o valor do resto for menor que 2 o resultado irá passar a valer 0
            }
            else
            {
                resultado = 11 - resultado; // caso o resultado seja maior que 2, irá pegar o valor do resultado e subtrair 11 Ex: (11-3)
            }

            CPF2 = resultado.ToString(); // o CPF irá receber o primeiro digito verificador.
            CPF1 += CPF2;

            soma = 0; // Reutilizar a variavel para o segundo digito.

            for (int j = 0; j < 10; j++) //utilizar o FOR para obter o vetor do array CPF inserido e multiplicar pelo vetor Digito2
            {
                soma += int.Parse(CPF1[j].ToString()) * Digito2[j];
            }

            resultado = soma % 11;

            if (resultado < 2)
            {
                resultado = 0;
            }
            else
            {
                resultado = 11 - resultado;
            }

            CPF2 += resultado.ToString(); // irá receber o segundo digito do CPF

            return cpf.EndsWith(CPF2); //ENDSWITH Determina se o final desta instância de cadeia de caracteres corresponde à cadeia de caracteres especificada.
        }

        public string ValidaRg(string rg)
        {
            string rgValido = string.Empty;
            try
            {
                rgValido = LimpaRG(rg);

                rgValido = ValidarRg(rgValido);

            }
            catch (Exception)
            {
                throw new Exception("O Rg informando não é válido" + rgValido);
            }

            return rgValido;
        }

        public string LimpaRG(string rg)
        {
            rg = rg.Trim();
            rg = Regex.Replace(rg, "[^a-zA-Z0-9]", "");

            return rg;
        }

        public string ValidarRg(string rg)
        {
            string novorg = rg.PadLeft(9, '0');
            return novorg;
        }

        public string ValidaEmail(string email)
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
