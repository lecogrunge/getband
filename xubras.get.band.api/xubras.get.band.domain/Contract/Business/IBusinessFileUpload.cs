namespace xubras.get.band.domain.Contract.Business
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using xubras.get.band.domain.Models.General;

    public interface IBusinessFileUpload
    {
        Task<HttpResponseMessage> Upload(IFormFile file, FileParameters parameters);
        Task<HttpResponseMessage> Delete(FileParameters parameters);
        Task<Dictionary<string, Dictionary<string, Stream>>> Download(FileParameters parameters);
    }
}
