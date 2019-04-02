using System.ComponentModel.DataAnnotations;

namespace xubras.get.band.domain.Entities
{
    public class InstrumentalistEntity
    {
        [Key]
        public int InstrumentalistID { get; set; }

        public string InstrumentalistName { get; set; }
        public string InstrumentalistNickName { get; set; }
        public string MusicalStyle { get; set; }
        public string Instrument { get; set; }
        public string Objective { get; set; }
        public string skills { get; set; }

    }
}