using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using xubras.get.band.api.Controllers.Base;
using xubras.get.band.data.Transactions;
using xubras.get.band.domain.Models.Band;

namespace xubras.get.band.api.Controllers
{
    [Route("api/band")]
    [ApiController]
    public class BandController : Controller
    {
        //private readonly IBandService _bandService;

        //public BandController(IUnitOfWork unityOfWork, IBandService bandService) : base(unityOfWork)
        //{
        //    _bandService = bandService;
        //}

        //[HttpPost]
        //public async Task<IActionResult> PostBand(CreateBandRequest band)
        //{
        //    CreateBandResponse response = _bandService.CreateBand(band);

        //    if (response.IsValid())
        //    {
        //        try
        //        {
        //            _unitOfWork.Commit();
        //            return Ok(response);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest($"Houve um problema interno com o servidor. Entre em contato com o Administrador do sistema caso o problema persista. Erro interno: {ex.Message}");
        //        }
        //    }

        //    return BadRequest(response.GetErrors());
        //}
    }
}