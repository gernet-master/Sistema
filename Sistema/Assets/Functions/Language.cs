/*
Descrição: Funções para retornar o texto solicitado no idioma correto
Data: 01/01/2021 - v.1.0
*/

using System;
using System.IO;
using System.Web;
using System.Xml;

namespace Functions
{
    // Idiomas do sistema
    public class Language
    {
        // Variáveis
        public string Text { get; set; }
        public string AccessKey { get; set; }

        // Recupera o texto
        public static Language XmlLang(int ident = 0, int format = 0)
        {
            Language l = new Language();
            Language x = readXML(ident);

            // Formata o texto
            l.Text = Utils.FormatString(x.Text, format);

            // Accesskey
            l.AccessKey = x.AccessKey;

            return l;
        }

        // Faz a leitura do Xml
        public static Language readXML(int ident)
        {
            Language x = new Language();

            // Seleção do arquivo
            string file = HttpContext.Current.Session["language"] as string;

            // Busca o idioma padrão do cliente
            //////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////
            if (String.IsNullOrEmpty(file))
            {
                file = "pt-br";
            }

            // Definição do arquivo do idioma
            string path = Path.Combine(System.AppContext.BaseDirectory + "Assets\\Languages\\", file + ".xml");

            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            // Seleção do item
            XmlNode node = xml.SelectSingleNode("//string[@id='" + ident + "']");

            if (node != null)
            {
                // Texto
                if (node.Attributes != null && node.Attributes["text"] != null)
                {
                    x.Text = node.Attributes["text"].Value;
                }
                else
                {
                    x.Text = "";
                }

                // Accesskey
                if (node.Attributes != null && node.Attributes["accesskey"] != null)
                {
                    x.AccessKey = node.Attributes["accesskey"].Value;
                }
                else
                {
                    x.AccessKey = "";
                }
            }
            else
            {
                x.Text = "";
                x.AccessKey = "";
            }

            return x;
        }

    }
}