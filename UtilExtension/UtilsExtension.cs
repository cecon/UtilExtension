using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace UtilExtension
{
    public static class UtilsExtension
    {
        /// <summary>
        /// Convert o string do argumento em um string MD5
        /// </summary>
        /// <param name="value">string a ser convertido em MD5</param>
        /// <returns>Um string MD5 do argumento.</returns>
        public static string ToMd5(this string value)
        {

            var resultado = value.Select(x => (byte)x).ToArray();
            return ToMd5(resultado);

        }

        /// <summary>
        /// Convert um array de bytes em um MD5
        /// </summary>
        /// <param name="value">array de bytes a ser convertido em MD5</param>
        /// <returns>Um string MD5 do argumento.</returns>
        public static string ToMd5(this byte[] value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            value = md5.ComputeHash(value);
            var sb = new StringBuilder();
            foreach (var t in value)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();

        }

        /// <summary>
        /// Transforma a primeira letra de cada palavra em uma maiúscula
        /// </summary>
        /// <param name="value">string com a frase a ser transformada.</param>
        /// <returns>String com as primeiras letras em maiúsculo</returns>
        public static string ToTitleCase(this string value)
        {
            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(value);
        }

        /// <summary>
        /// Converte o string do argumento em um HASH base 64
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <returns>string da base 64</returns>
        public static string ToBase64(this string value)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
        }

        /// <summary>
        /// Converte o array de byte do argumento em um HASH base 64
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <returns>string da base 64</returns>
        public static string ToBase64(this byte[] value)
        {
            return Convert.ToBase64String(value);
        }

        /// <summary>
        /// Descriptografa um string em base64
        /// </summary>
        /// <param name="value">string com a base 64 a ser descriptografado.</param>
        /// <returns>String sem a criptografia</returns>
        public static string FromBase64(this string value)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(value));
        }

        /// <summary>
        /// Remove qualquer caracter qua não seja um digito
        /// </summary>
        /// <param name="args">string a ser tratado</param>
        /// <returns>string apenas com digitos.</returns>
        public static string ToDigit(this string args)
        {
            return args.Where(Char.IsDigit).Aggregate("", (current, c) => current + c.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Remove qualquer caracter que não seja uma letra do alfabeto        
        /// </summary>
        /// <param name="args">string a ser tratado</param>
        /// <returns>string apenas com letras</returns>
        public static string ToLetter(this string args)
        {
            return args.Where(Char.IsLetter).Aggregate("", (current, c) => current + c.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Remove qualquer caracter que seja um letra ou um digito
        /// permanecendo apenas os simbolos.
        /// </summary>
        /// <param name="args">string a ser tratado</param>
        /// <returns>string apenas com simbolos</returns>
        public static string ToSymbol(this string args)
        {
            return args.Where(c => !Char.IsLetterOrDigit(c)).Aggregate("", (current, c) => current + c.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Convert um string em um decimal.
        /// </summary>
        /// <example>
        /// args = 25002
        /// valor de retorno = 250,02
        /// ou
        /// args = 250,023
        /// valor de retorno = 2500,23
        /// Utilizado para formatar um campo de texto em valor monetário
        /// </example>
        /// <param name="args">valor a ser formatado</param>
        /// <param name="casasDecimais">Número de casas decimais após a virgula</param>
        /// <returns>string no formato monetário</returns>
        public static decimal ToMoney(this string args, int casasDecimais)
        {
            if (string.IsNullOrEmpty(args)) return 0;

            var sign = (args[0] == '-' || args[0] == '(' ? "-" : "");
            var n = ToDigit(args).PadLeft(19, '0');

            return
                Convert.ToDecimal(string.Format("{0}{1},{2}", sign, n.Substring(0, n.Length - casasDecimais),
                    n.Substring(n.Length - casasDecimais)));
        }

        /// <summary>
        /// Convert um string em um decimal com 2 casas decimais.
        /// </summary>
        /// <example>
        /// args = 25002
        /// valor de retorno = 250,02
        /// ou
        /// args = 250,023
        /// valor de retorno = 2500,23
        /// Utilizado para formatar um campo de texto em valor monetário
        /// </example>
        /// <param name="args">valor a ser formatado</param>
        /// <returns>string no formato monetário</returns>
        public static decimal ToMoney(this string args)
        {
            return ToMoney(args, 2);
        }

        /// <summary>
        /// Mascara um string conforme o argumento MASK
        /// </summary>
        /// <param name="mask">mascara a ser passada para o string do argumento.</param>
        /// <param name="value">valor a ser mascarado.</param>
        /// <param name="culture">Informe o modelo cultural de formatação</param>
        /// <returns>retorna o valor informado no argumento devidamente mascarado.</returns>
        public static string ToMask(this string value, string mask, CultureInfo culture)
        {
            var t = new MaskedTextBox { Mask = mask, Text = value, Culture = culture };
            return t.Text;
        }

        /// <summary>
        /// Mascara um string conforme o argumento MASK
        /// </summary>
        /// <param name="mask">mascara a ser passada para o string do argumento.</param>
        /// <param name="value">valor a ser mascarado.</param>
        /// <returns>retorna o valor informado no argumento devidamente mascarado.</returns>
        public static string ToMask(this string value, string mask)
        {
            return ToMask(value, mask, CultureInfo.GetCultureInfo("en-US"));
        }

        /// <summary>
        /// Transforma um Tipo Em outro Tipo
        /// </summary>
        /// <typeparam name="T">Tipo no qual o objeto deverá ser convertido</typeparam>
        /// <param name="value">Objeto a ser convertido</param>
        /// <returns>Tipo solicitado</returns>
        public static T ToType<T>(this object value)
        {
            var json = value.ToJson();
            var result = FromJson<T>(json);
            return result;
        }

        /// <summary>
        /// Transforma o tipo a partir de um outro tipo
        /// </summary>
        /// <typeparam name="T">Tipo no qual o objeto deverá ser convertido</typeparam>
        /// <param name="value">Objeto solicitante</param>
        /// <param name="obj">Objeto a ser transformado</param>
        /// <returns>Objeto convertido</returns>
        public static T FromType<T>(this T value, object obj)
        {
            var json = obj.ToJson();
            return FromJson<T>(json);
        }

        /// <summary>
        /// Convert qualquer objeto em um string json
        /// </summary>
        /// <param name="value">Objeto a ser serializado</param>
        /// <returns>string em formato json com a representação do objeto</returns>
        public static string ToJson(this object value)
        {
            return ToJson(value, int.MaxValue);
        }

        /// <summary>
        /// Convert qualquer objeto em um string json
        /// </summary>
        /// <param name="value">Objeto a ser serializado</param>
        /// <param name="maxJsonLength">tamanho máximo do JSON</param>
        /// <example>int.MaxValue ou 3000</example>
        /// <returns>string em formato json com a representação do objeto</returns>
        public static string ToJson(this object value, int maxJsonLength)
        {
            var ser = new JavaScriptSerializer { MaxJsonLength = maxJsonLength };
            return ser.Serialize(value);
        }

        /// <summary>
        /// Convert qualquer string json em seu respectivo tipo
        /// </summary>
        /// <param name="value">String a ser deserializado</param>
        /// <returns>Tipo com o string json desserializado</returns>
        public static T FromJson<T>(this string value)
        {
            return FromJson<T>(value, int.MaxValue);
        }
        /// <summary>
        /// Convert qualquer string json em seu respectivo tipo
        /// </summary>
        /// <typeparam name="T">Tipo do objeto</typeparam>
        /// <param name="value">String a ser deserializado</param>
        /// <param name="maxJsonLength">Tamanho do json a ser construido</param>
        /// <returns>Tipo com o string json desserializado</returns>
        public static T FromJson<T>(this string value, int maxJsonLength)
        {
            var sr = new JavaScriptSerializer { MaxJsonLength = maxJsonLength };
            return sr.Deserialize<T>(value);
        }

        /// <summary>
        /// Popula uma objeto com os dados de propriedades de outro objeto
        /// </summary>
        /// <typeparam name="T">Tipo do objeto que será populado</typeparam>
        /// <param name="to">objeto que será populado</param>
        /// <param name="from">objeto que será transferido</param>
        /// <returns>Tipo atualizado</returns>
        public static void MapFromTo<T>(this T to, object from)
        {
            if (@from == null) return;
            var properties = to.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var prop = from.GetType().GetProperty(propertyInfo.Name);
                if (prop == null) continue;
                var pVal = prop.GetValue(from, null);
                propertyInfo.SetValue(to, pVal, null);
            }
        }
    }
}