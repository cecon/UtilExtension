using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace UtilExtension {
    public static class CheckerExtension {
        /// <summary>
        /// Verifica se todos os valores dentro do argumento values coferem com os valores permitidos.
        /// </summary>
        /// <example>
        /// EX: permits = "1234567890ABCDEFGHIJKLMNOPQRSTUVXZ"
        ///     value = "NOMEÇ123"
        ///     nesse caso o valor de retorno seria falso, tendo em vista que o Ç informado
        /// no campo value não confere com nenhum dos caractere do argumento permits
        /// </example>
        /// <param name="allowed">string de caracteres permitidos</param>
        /// <param name="value">valor a ser conferido</param>
        /// <returns>retorna um bool indicando se os valores conferem ou não!</returns>
        public static bool IsMatch(this string value, string allowed)
        {
            var result = value.All(allowed.Contains);
            return result;
        }

        /// <summary>
        /// Verifica se todos os caracteres contidos no argumento são numéricos.
        /// </summary>
        /// <param name="args">Caracteres a serem analizados</param>
        /// <returns>Bool indicando se o argumento contem apenas digitos ou não</returns>
        public static bool AreDigits(this string args)
        {
            return Regex.IsMatch(args, "^[0-9]*$");            
        }

        /// <summary>
        /// Verifica se todos os caracteres contidos no argumento são 
        /// numéricos ou letras válidas do alfabeto
        /// </summary>
        /// <param name="args">Caracteres a serem validados</param>
        /// <returns>Bool indicando se os caracteres são apenas números ou letras</returns>
        public static bool IsLetterOrDigit(this string args) {
            return IsLetterOrDigit(args, "");
        }

        /// <summary>
        /// Verifica se todos os caracteres contidos no argumento são 
        /// numéricos ou letras válidas do alfabeto
        /// </summary>
        /// <param name="args">Caracteres a serem validados</param>
        /// <param name="exception">Caracteres que devem ser ignorados extra alfabeto</param>
        /// <returns>Bool indicando se os caracteres são apenas números ou letras</returns>
        public static bool IsLetterOrDigit(this string args, string exception) {
            return IsMatch(args, string.Concat("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYX", exception));
        }

        /// <summary>
        /// Verifica se no argumento existe um CNPJ válido
        /// o sistema irá ignorar qualquer tipo de formatação tratando apenas números
        /// </summary>
        /// <param name="cnpj">CNPJ a ser validado</param>
        /// <returns>Bool indicando se o argumento é ou não um CNPJ</returns>
        public static bool CheckCnpj(this string cnpj) {
            var multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            cnpj = cnpj.ToDigit();
            if (cnpj.Length != 14) return false;

            var tempCnpj = cnpj.Substring(0, 12);
            var soma = 0;
            for (var i = 0; i < 12; i++) soma += int.Parse(tempCnpj[i].ToString(CultureInfo.InvariantCulture)) * multiplicador1[i];
            var resto = (soma % 11);
            if (resto < 2) resto = 0;
            else resto = 11 - resto;

            var digito = resto.ToString(CultureInfo.InvariantCulture);
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (var i = 0; i < 13; i++) soma += int.Parse(tempCnpj[i].ToString(CultureInfo.InvariantCulture)) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            digito = digito + resto.ToString(CultureInfo.InvariantCulture);
            return cnpj.EndsWith(digito);
        }

        /// <summary>
        /// Verifica se no argumento existe um CPF válido
        /// o sistema irá ignorar qualquer tipo de formatação tratando apenas números
        /// </summary>
        /// <param name="cpf">CPF a ser validado</param>
        /// <returns>Bool indicando se o argumento contem ou não um CPF válido</returns>
        public static bool CheckCpf(this string cpf) {
            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            cpf = cpf.ToDigit();
            if (cpf.Length != 11) return false;

            var tempCpf = cpf.Substring(0, 9);
            var soma = 0;
            for (var i = 0; i < 9; i++) soma += int.Parse(tempCpf[i].ToString(CultureInfo.InvariantCulture)) * multiplicador1[i];
            var resto = soma % 11;
            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            var digito = resto.ToString(CultureInfo.InvariantCulture);
            tempCpf = tempCpf + digito;
            soma = 0;
            for (var i = 0; i < 10; i++) soma += int.Parse(tempCpf[i].ToString(CultureInfo.InvariantCulture)) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            digito = digito + resto.ToString(CultureInfo.InvariantCulture);
            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Verifica se é um Email válido
        /// </summary>
        /// <param name="value">email a ser validado</param>
        /// <returns>Bool indicando se o e-mail está correto</returns>
        public static bool CheckEmail(this string value)
        {
            return Regex.IsMatch(value, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum)\b");
        }
    }   
}