namespace xubras.get.band.domain.Domains.User
{
    using System.Collections.Generic;
    using xubras.get.band.domain.ValueObjects;

    public sealed class Fan
    {
        protected Fan() { }

        public IList<Band> Bands { get; private set; }
    }
}