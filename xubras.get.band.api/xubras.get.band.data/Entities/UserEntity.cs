using System;
using System.ComponentModel.DataAnnotations;

namespace xubras.get.band.data.Entities
{
    public sealed class UserEntity
    {
        [Key]
        public Guid IdUser { get;  set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }
    }
}