using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoteliTest.DTO
{
    public class StavkaRacunaDTO
    {
        public int StavkaRacunaID { get; set; }
        public int Kolicina { get; set; }
        public int RacunID { get; set; }
        public int UslugaID { get; set; }
    }
}