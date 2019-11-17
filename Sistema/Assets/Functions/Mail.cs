/*
Descrição: Funções para envio de email
Data: 01/01/2020 - v.1.0
*/

using System;
using System.Net;
using System.Net.Mail;

namespace Functions
{
    public class Mail
    {
        // Envia o e-mail do sistema
        public static Boolean EnviaEmail(string para = "", string titulo = "", string mensagem = "")
        {
            Boolean ret = false;

            try
            {
                //SmtpClient smtp = new SmtpClient();
                //NetworkCredential credenciais = new NetworkCredential("sender@gestaodental.com.br", "tes12345");
                //MailMessage msg = new MailMessage();
                //MailAddress from = new MailAddress("sender@gestaodental.com.br");
                //smtp.Host = "smtp.zoho.com";
                //smtp.EnableSsl = true;
                //smtp.Port = 587;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = credenciais;
                //msg.From = new MailAddress("sender@gestaodental.com.br"); 
                //msg.Subject = titulo;
                //msg.IsBodyHtml = true;
                //msg.Body = mensagem;
                //msg.To.Add(para);

                //smtp.Send(msg);

                ret = true;
            }
            catch (Exception) { }

            return ret;
        }       

    }
}

