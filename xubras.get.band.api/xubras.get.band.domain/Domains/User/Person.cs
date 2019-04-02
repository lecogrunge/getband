namespace xubras.get.band.domain.Domains.User
{
    using System;
    using System.Collections.Generic;
    using xubras.get.band.domain.Enums;

    public sealed class Person 
    {
        protected Person() { } 

        public DateTime Bith { get; private set; }
        public Gender Gender { get; private set; }
        public List<Style> MusicalStyles { get; private set; }
        public bool UseTerm { get; private set; }
    }
}