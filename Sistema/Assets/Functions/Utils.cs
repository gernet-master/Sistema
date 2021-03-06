﻿/*
Descrição: Funções do sistema
Data: 01/01/2021 - v.1.0
*/

using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace Functions
{
    public class Utils
    {
        // Formata a string
        public static string FormatString(string text = "", int format = 0)
        {
            string result = "";
            if (text.Length > 0)
            {
                switch (format)
                {
                    // Tudo minúsculo
                    case 0:
                        result = text.ToLower();
                        break;

                    // Tudo maíusculo
                    case 1:
                        result = text.ToUpper();
                        break;

                    // Primeira letra em maíusculo, resto em minúsculo
                    case 2:
                        result = char.ToUpper(text[0]) + text.Substring(1).ToLower();
                        break;

                    // Sem formatação, padrão digitado
                    case 3:
                        result = text;
                        break;

                    // Primeira letra de cada palavra em maíusculo, resto em minúsculo
                    case 4:
                        result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
                        break;
                }
            }
            return result;
        }

        // Retorna somente os numeros
        public static int Numbers(string text)
        {
            return Convert.ToInt32(new string(text.Where(char.IsNumber).ToArray()));
        }

        // Tratamento de valor nulo
        public static string Null(string text = "", string value = "")
        {
            string result = "";
            if (String.IsNullOrEmpty(text))
            {
                result = value;
            }
            else
            { 
                result = text;
            }
            return result;
        }

        // Substitui alguns caracteres do texto por códigos, para segurança ao gravar no banco de dados e limita o tamanho do texto
        public static string ClearText(string text = "", int limit = 0)
        {
            string result = "";
            result = text.Trim();
            result = result.Replace("'", "&apos;");
            result = result.Replace("<", "&lt;");
            result = result.Replace(">", "&gt;");
            result = result.Replace("\"", "&quot;");
            if ((limit > 0) && (text.Length > limit))
            {
                result = result.Substring(0, limit);
            }
            return result;
        }

        // Remove acentos da string
        public static string RemoveDiacritics(string text = "")
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        // Pega o ip do cliente
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        // Cria uma senha aleatória
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Verifica se o idsession existe na aplicação
        public static Boolean Session(string idsession)
        {
            if (idsession.Trim().Length == 0)
            {
                return false;
            }
            else
            {
                string[] sessions = HttpContext.Current.Application["sessions"].ToString().Split(',');
                return Array.Exists(sessions, element => element == idsession);
            }
        }

        // Carrega a imagem do avatar ou padrão do sistea
        public static string Avatar(string txfoto)
        {
            if (Utils.Null(txfoto, "") == "")
            {
                return "/Assets/Media/users/default.jpg";
            }
            else
            {
                return txfoto;
            }
        }

        // Formata campo para exibição em listagem
        public static string formatField(dynamic value, string type)
        {
            string ret = "";

            switch (type)
            {
                // Exibe icone V/X para informar
                case "boolean":
                    ret = (Convert.ToBoolean(value) ? "<i class='fa fa-check ft-green'></i>" : "<i class='fa fa-times ft-red'></i>");
                    break;
                
                // Carrega a partir do arquivo de idioma
                case "language":
                    ret = Language.XmlLang(Convert.ToInt32(value), 2).Text;
                    break;

                default:
                    ret = Convert.ToString(value);
                    break;
            }

            return ret;
        }        
    }
}