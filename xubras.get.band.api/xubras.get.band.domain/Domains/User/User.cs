namespace xubras.get.band.domain.Domains.User
{
    using System;
    using xubras.get.band.domain.ValueObjects;

    public class User
    {
        protected User() { }

        public User(Email email, string password, string name, string nickName) : this(email, password)
        {
            IdUser = Guid.NewGuid();
            NickName = nickName;
            Name = name;
            Password = password;
        }

        public User(Email email, string password)
        {
            Email = email;
            Password = password;
        }


        public Guid IdUser { get; private set; }

        public Email Email { get; private set; }

        public string Password { get; private set; }

        public string Name { get; private set; }

        public string NickName { get; private set; }

        
        //public string Phone { get; private set; }

        //public string Celphone { get; private set; }

        //public Photo Photo { get; private set; }        

        //public Nationality Nationality { get; private set; }

        //public City City { get; private set; }

        //public string Description { get; private set; }
    }
}