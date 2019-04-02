namespace xubras.get.band.api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using xubras.get.band.api.Controllers.Base;
    using xubras.get.band.data.Transactions;
    using xubras.globalization;

    [Route("Globalization")]
    [ApiController]
    public class TranslateController : BaseController
    {
        public TranslateController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [Route("Translate/{key}")]
        [HttpGet]
        public async Task<IActionResult> Translate(string key, string culture = null)
        {
            return Ok(string.IsNullOrEmpty(culture) ? key.Translate() : key.Translate(new CultureInfo(culture)));
        }

        [Route("Translate/{keys}")]
        [HttpGet]
        public async Task<IActionResult> Translate(List<string> keys, string culture = null)
        {
            return Ok(string.IsNullOrEmpty(culture) ? keys.Translate() : keys.Translate(new CultureInfo(culture)));
        }
    }
}