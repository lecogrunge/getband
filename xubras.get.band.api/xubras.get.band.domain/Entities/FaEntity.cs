using System;
using System.ComponentModel.DataAnnotations;

namespace xubras.get.band.domain.Entities
{
    public class FaEntity
    {
        [Key]
        public int FaId { get; set; }

        public string NameFa { get; set; }
        public string NicknameFa { get; set; }
        public string GenderFa { get; set; }
        public string AdressFa { get; set; }
        public string CityFa { get; set; }
        public string StateFa { get; set; }
        public DateTime BirthdayFa { get; set; }
        public int CelPhoneFa { get; set; }
        public int ResidencialPhohe { get; set; }
        public string EmailFa { get; set; }
        public int CpfCnpjFa { get; set; }
    }
}