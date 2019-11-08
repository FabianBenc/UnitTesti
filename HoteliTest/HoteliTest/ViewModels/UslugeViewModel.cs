using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoteliTest.ViewModels
{
    public class UslugeViewModel
    {
        public int RezervacijaID { get; set; }
        public int RacunID { get; set; }
        public decimal Popust { get; set; }
        public string Prezime { get; set; }
        public DateTime Prijava { get; set; }
        public DateTime Odjava { get; set; }
        public decimal CijenaRezervacije { get; set; }
        public StavkaRacuna StavkaRacuna { get; set; }
        public Usluga Usluga { get; set; }
        public Rezervacija Rezervacija { get; set; }
    }
}