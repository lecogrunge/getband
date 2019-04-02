namespace xubras.get.band.domain.Domains.User
{
    using System.Collections.Generic;

    public sealed class Instrumentist
    {
        protected Instrumentist() {}
        public IList<Instrument> Instruments { get; private set; }
        public string Objetivo { get; private set; }
        public IList<Skills> Habilidades { get; private set; }        
    }
}