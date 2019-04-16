using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using xubras.get.band.domain.Contract.Services;

namespace xubras.get.band.domain.Services
{
    public sealed class EmailService : IEmailService
    {
        private const string _emailSender = "getbandsender@gmail.com";
        private const string _password = "i?q25Dx8";
        private const string _smtp = "smtp.gmail.com";
        private const int _port = 587;

        public async Task SendEmailCreateUser(string to, string name, Guid token)
        {
            string formatMessage = string.Format(@"Olá <strong>{0}</strong>, estamos quase lá!<br />
                                                   Clique no link a seguir para confirmar seu cadastro no GetBand. <a href='https://www.getband.com.br/confirm/{1}' target='_blank'>Confirmar cadastro</a><br /><br />
                                                   Caso você não tenha se cadastrado em nossa plataforma, por favor ignorar este e-mail.", name, token);

            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSender, "GetBand")
                };

                mail.To.Add(new MailAddress(to));
                //mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = "[GetBand] - Confirmação de cadastro";
                mail.Body = formatMessage;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_smtp, _port))
                {
                    smtp.Credentials = new NetworkCredential(_emailSender, _password);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}