using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoteliTest.DTO
{
    public class RacunDTO
    {
        public int RacunID { get; set; }
        public int RezervacijaID { get; set; }
        public bool Placeno { get; set; }
        public double IznosUkupno { get; set; }
    }
}