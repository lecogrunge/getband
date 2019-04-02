using Microsoft.AspNetCore.Mvc;
using xubras.get.band.data.Transactions;

namespace xubras.get.band.api.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}