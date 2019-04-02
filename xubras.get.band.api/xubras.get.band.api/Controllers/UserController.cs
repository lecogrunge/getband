using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using xubras.get.band.api.Controllers.Base;
using xubras.get.band.data.Transactions;
using xubras.get.band.domain.Contract.Business;
using xubras.get.band.domain.Models.User;
using xubras.globalization;

namespace xubras.get.band.api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IBusinessUser _businessUser;

        public UserController(IUnitOfWork unityOfWork, IBusinessUser businessUser) : base(unityOfWork)
        {
            _businessUser = businessUser;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(CreateUserRequest user)
        {
            CreateUserResponse response = _businessUser.CreateUser(user);

            if (response.IsValid())
            {
                try
                {
                    _unitOfWork.Commit();
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return BadRequest("ErrorGeneric400".Translate());
                }
            }

            return BadRequest(response.GetErrors());
        }
    }
}