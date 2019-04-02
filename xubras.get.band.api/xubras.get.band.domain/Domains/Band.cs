using System;

namespace xubras.get.band.domain.Domains
{
    public sealed class Band
    {
        protected Band() { }

        public Guid IdBand { get; private set; }
        public string Name { get; private set; }
    }
}