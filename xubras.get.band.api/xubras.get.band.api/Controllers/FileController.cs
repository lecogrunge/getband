namespace xubras.get.band.api.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using xubras.get.band.api.Controllers.Base;
    using xubras.get.band.data.Transactions;
    using xubras.get.band.domain.Business;
    using xubras.get.band.domain.Contract.Business;
    using xubras.get.band.domain.Models.General;
    using xubras.get.band.domain.Util;

    /// <summary>
    ///     Send or receive files
    /// </summary>
    [ApiController]
    [Route("file")]
    public class FileController : BaseController
    {
        #region [ Attributes ]

        private readonly BusinessFileUpload _businessFileUpload;
        private readonly Configuration _configuration;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="businessFileUpload"></param>
        public FileController(IOptions<Configuration> configuration, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _configuration = configuration.Value;
            _businessFileUpload = new BusinessFileUpload(configuration);
        }

        #endregion

        #region [ GET ]

        /// <summary>
        ///     get files reference
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> File([FromQuery] FileParameters parameters)
        {
            return Ok(await _businessFileUpload.List(parameters));
        }

        /// <summary>
        ///     get file detail
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Route("detail")]
        [HttpGet]
        public async Task<IActionResult> FileDetail([FromQuery] FileParameters parameters)
        {
            return Ok(_businessFileUpload.List(parameters, true));
        }

        /// <summary>
        ///     Rotina para obter os arquivos salvos no servidor
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Route("download")]
        [HttpGet]
        public async Task<IActionResult> FileDownload([FromQuery] FileParameters parameters)
        {
            var file = await _businessFileUpload.Download(parameters);
            var key = file["File"].Keys.Take(1).Select(d => d).First();
            var streamFile = file["File"].Values.Take(1).Select(d => d).First();

            return File(streamFile, key);
        }

        #endregion

        #region [ POST ]

        /// <summary>
        ///     Rotina de envio de arquivos para salvar no servidor verificando:
        ///      - Caso haja restrição de extensões
        ///      - O caminho(path) enviado
        ///      - Validação da extensão de arquivo
        ///      - Atribuição do arquivo em um stream e verifica se existe arquivo a ser enviado
        /// </summary>
        /// <param name="file"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Route("upload")]
        [HttpPost]
        public Task<HttpResponseMessage> FileUpload(IFormFile file, [FromQuery] FileParameters parameters)
        {
            return _businessFileUpload.Upload(file, parameters);
        }

        #endregion

        #region [ DELETE ]

        /// <summary>
        ///     Rotina para excluir o arquivo enviado ao servidor
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Route("delete")]
        [HttpDelete]
        public async Task<HttpResponseMessage> FileDelete([FromQuery] FileParameters parameters)
        {
            return await _businessFileUpload.Delete(parameters);
        }

        #endregion
    }
}
