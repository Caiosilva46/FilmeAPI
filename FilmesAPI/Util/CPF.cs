using System;
using System.Text.RegularExpressions;

namespace FilmesAPI.Models.ValueObject
{
    public class CPF
    {
        public string cpfValido = string.Empty;
        public CPF()
        {
            
        }

        public string ValidaCpf(string cpf)
        {
            try
            {
                cpfValido = LimpaCPF(cpf);

                cpfValido = ValidarTamanhoCPf(cpfValido);

                //metodo que verificar CPF (cpf)    
                if (!VerificarCPf(cpfValido))
                    throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception("O CPF informando não é válido" + cpfValido);
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
    }
}
