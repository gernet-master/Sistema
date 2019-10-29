/*
Descrição: Funções de formatação de dados
Data: 01/01/2020 - v.1.0
*/

using System.Globalization;

namespace Functions
{
    public class String
    {
        // Formata a string
        public static string FormatString(string text = "", int format = 0)
        {
            string result = "";
            if (text.Length > 0)
            {
                switch (format)
                {
                    case 0:
                        result = text.ToLower();
                        break;
                    case 1:
                        result = text.ToUpper();
                        break;
                    case 2:
                        result = char.ToUpper(text[0]) + text.Substring(1).ToLower();
                        break;
                    case 3:
                        result = text;
                        break;
                    case 4:
                        result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
                        break;
                }
            }
            return result;
        }

        // Tratamento de valor nulo
        public static string Null(string text = "", string value = "")
        {
            string result = "";
            if (text == null)
            {
                result = value;
            }
            else
            {
                result = text;
            }
            return result;
        }

        // Substitui alguns caracteres do texto por códigos, para segurança ao gravar no banco de dados e limitar tamanho do texto
        public static string ClearText(string text = "", int limit = 0)
        {

            string result = "";
            result = text.Trim();
            result = result.Replace("'", "&apos;");
            result = result.Replace("<", "&lt;");
            result = result.Replace(">", "&gt;");
            result = result.Replace("\"", "&quot;");
            if (limit > 0)
            {
                result = result.Substring(0, limit);
            }
            return result;
        }
        
    }
}