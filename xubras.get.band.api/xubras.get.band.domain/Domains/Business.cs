namespace xubras.get.band.domain.Domains
{
    using System;
    using System.Collections.Generic;

    public sealed class Business
    {
        protected Business() { }

        public DateTime FoundationDate { get; private set; }
        public List<Photo> Gallery { get; private set; }
        public Schedule Schedule { get; private set; }
        public bool UseTerm { get; private set; }
    }
}