using System.Collections.Generic;
using xubras.get.band.domain.Domains;
using xubras.get.band.domain.Models.Base;

namespace xubras.get.band.domain.Models.Band
{
    public sealed class GetProfileBandResponse : ResponseBase
    {
        public string Name { get; set; }
        public IEnumerable<Photo> Gallery { get; set; }
        // etc...
    }
}