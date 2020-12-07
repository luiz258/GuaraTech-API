using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;
using System.Net;
using GuaraTech.Models;

namespace GuaraTech.Services
{
    public class EmailService:IEmailService
    {
        private readonly IEmailbody _body;
        public EmailService(IOptions<EmailSettings> emailSettings, IEmailbody body)
        {
            _emailSettings = emailSettings.Value;
            _body = body;
        }

        public EmailSettings _emailSettings { get; }
        public bool Send(string nome, string email, string assunto, string msg)
        {
            try
            {
                ///$"<b>Nome:</b> { nome }<br><b>Email:</b> { email }<br><b>Assunto:</b> { assunto }<br><b>Senha:</b> { msg }"

                MailMessage mailMessage = new MailMessage();
                //Endereço que irá aparecer no e-mail do usuário 
                mailMessage.From = new MailAddress(_emailSettings.FromEmail);
                mailMessage.To.Add(email);
                //destinatarios do e-mail, para incluir mais de um basta separar por ponto e virgula 
                mailMessage.Subject = assunto;
                mailMessage.IsBodyHtml = true;
                //conteudo do corpo do e-mail 
                mailMessage.Body =  _body.BodyEmail(nome,msg);
                mailMessage.Priority = MailPriority.High;
                //smtp do e-mail que irá enviar 
                SmtpClient smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    //credenciais da conta que utilizará para enviar o e-mail 
                    Credentials = new NetworkCredential("guaratechoficial@gmail.com", "guaratech@2020")
                };

                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
