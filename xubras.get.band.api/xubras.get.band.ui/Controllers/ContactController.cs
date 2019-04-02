using Microsoft.AspNetCore.Mvc;
using xubras.get.band.ui.Controllers.Base;

namespace xubras.get.band.ui.Controllers
{
    public class ContactController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}