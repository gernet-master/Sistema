/*
Descrição: Funções para retornar o texto solicitado no idioma correto
Data: 01/01/2020 - v.1.0
*/

using System.IO;
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

            // Definição do arquivo do idioma
            string path = Path.Combine(System.AppContext.BaseDirectory + "Assets\\Languages\\", "pt-br.xml");

            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            // Seleção do item
            XmlNode node = xml.SelectSingleNode("//string[@id='" + ident + "']");

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

            return x;
        }

    }
}