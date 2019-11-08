using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoteliTest.ViewModels
{
    public class RezervacijaView
    {
        public int RezervacijaID { get; set; }
        public DateTime Prijava { get; set; }
        public DateTime Odjava { get; set; }
        public int GostID { get; set; }
        public int SobaID { get; set; }
        public int BrojSobe { get; set; }
        public decimal? Popust { get; set; }
        public bool Rezervirano { get; set; }
        public IEnumerable<Gost> Gosti { get; set; }
        public IEnumerable<Soba> Sobe { get; set; }
    }
}