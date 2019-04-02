using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using xubras.get.band.domain.Contract.Services;

namespace xubras.get.band.domain.Services
{
    public sealed class EmailService : IEmailService
    {
        private const string _host = "smtp.gmail.com";
        private const int _port = 587;
        private const string _hostMail = "";
        private const string _hostPassword = "";

        public void SendEmail(string to, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _host,
                Port = _port,
                EnableSsl = true,
                Credentials = new NetworkCredential(_hostMail, _hostPassword)
            };

            using (var message = new MailMessage(_hostMail, to)
            {
                Subject = subject,
                Body = body
            })
            {
                //await smtpClient.SendMailAsync(message);
            }
        }
    }
}