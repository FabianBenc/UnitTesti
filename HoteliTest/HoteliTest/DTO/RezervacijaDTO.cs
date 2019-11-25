using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoteliTest.DTO
{
    public class RezervacijaDTO
    {
        public int RezervacijaID { get; set; }
        public int GostID { get; set; }
        public int SobaID { get; set; }
        public decimal Popust { get; set; }

        public bool Rezervirano { get; set; }
        public DateTime Prijava { get; set; }
        public DateTime Odjava { get; set; }
    }
}