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
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("sender@gestaodental.com.br");
                message.To.Add(new MailAddress(para));
                message.Subject = titulo;
                message.IsBodyHtml = true;
                message.Body = mensagem;
                smtp.Port = 465;
                smtp.Host = "smtp.zoho.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("sender@gestaodental.com.br", "tes12345");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                ret = true;
            }
            catch (Exception) { }

            return ret;
        }       

    }
}