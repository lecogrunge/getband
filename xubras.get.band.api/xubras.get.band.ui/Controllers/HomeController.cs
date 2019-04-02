using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using xubras.get.band.ui.Controllers.Base;
using xubras.get.band.ui.Models;

namespace xubras.get.band.ui.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
