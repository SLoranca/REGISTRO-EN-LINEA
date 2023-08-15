using SEyTEvent.Models;
using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web.Configuration;

namespace SEyTEvent.Services
{
    public class SendEmail : Response
    {
        string credencial;
        string contraseña;

        public SendEmail()
        {
            credencial = WebConfigurationManager.AppSettings["credencial"].ToString();
            contraseña = WebConfigurationManager.AppSettings["SMTP_SEyT"].ToString();
        }

        public Response _SendEmail(string EmailDestino, string asunto, string body, string folio)
        {
            Response response = new Response();
            try
            {
                string EmailOrigen = credencial;
                string Contraseña = contraseña;

                MailMessage oMailMessage = new MailMessage();

                oMailMessage.Subject = asunto;
                oMailMessage.From = new MailAddress(EmailOrigen);
                oMailMessage.To.Add(new MailAddress(EmailDestino));

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.Html);
                oMailMessage.AlternateViews.Add(htmlView);

                oMailMessage.IsBodyHtml = true;
                SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
                oSmtpClient.EnableSsl = true;
                oSmtpClient.UseDefaultCredentials = false;
                oSmtpClient.Port = 587;
                oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);

                oSmtpClient.Send(oMailMessage);
                oSmtpClient.Dispose();

                response.data = folio;
                response.status = "OK";
                response.message = "Correo enviado exitosamente";
            }
            catch (Exception ex)
            {
                response.status = "ERROR";
                response.message = ex.Message.ToString();
            }

            return response;
        }
    }
}