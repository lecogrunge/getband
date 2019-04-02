using System;
using xubras.get.band.domain.Models.Base;

namespace xubras.get.band.domain.Models.User
{
    public sealed class CreateUserResponse : ResponseBase
    {
        public Guid IdUser { get; set; }
    }
}