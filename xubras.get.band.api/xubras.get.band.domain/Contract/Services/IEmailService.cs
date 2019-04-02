namespace xubras.get.band.domain.Contract.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}