using System;
using System.Threading.Tasks;

namespace xubras.get.band.domain.Contract.Services
{
    public interface IEmailService
    {
        Task SendEmailCreateUser(string to, string name, Guid token);
    }
}