using HoteliTest.Models;
using HoteliTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockData
{
    public class ModelLoader
    {
        public static ICollection<Gost> GetGosti()
        {
            return new List<Gost>
            {
                new Gost{GostID=1,Ime="Test1",Prezime="Test1",Email="Test1@test1.com",Adresa="Test1Adresa" },
                new Gost{GostID=2,Ime="Test2",Prezime="Test2",Email="Test2@test2.com",Adresa="Test2Adresa" }
            };
        }

        public static Gost GetInvalidGost()
        {
            return new Gost
            {
                GostID=1000,
                Ime="Test1000"
            };
        }
        public static Gost GetValidGost()
        {
            return new Gost
            {
                GostID=2,
                Ime="Test1",
                Prezime="Test1",
                Email="Test1@Test1.com",
                Adresa="Test1Adresa"
            };
        }

        public static ICollection<Hotel> GetHoteli()
        {
            return new List<Hotel>
            {
                new Hotel{HotelID=1,Ime="Test1",Lokacija="Test1",Adresa="Test1" },
                new Hotel{HotelID=2,Ime="Test2",Lokacija="Test2",Adresa="Test2" }
            };
        }

        public static Hotel GetInvalidHotel()
        {
            return new Hotel
            {
                HotelID = 1000,
                Ime = "test1000"
            };
        }

        public static Hotel GetValidHotel()
        {
            return new Hotel
            {
                HotelID=2,
                Ime="TestIme",
                Lokacija="TestLokacija",
                Adresa="TestAdresa"
            };
        }

        public static ICollection<Soba> GetSobe()
        {
            return new List<Soba>
            {
                
                new Soba{SobaID=1,HotelID=1,TipSobeID=1,BrojSobe=100},
                new Soba{SobaID=2,HotelID=1,TipSobeID=1,BrojSobe=200 }
            };
        }

        public static Soba GetInvalidSoba()
        {
            return new Soba
            {
                SobaID = 100,
                TipSobeID = 2,
                HotelID = 1,
            };
        }

        public static Soba GetValidSoba()
        {
            return new Soba
            {
                SobaID = 100,
                BrojSobe = 3,
                TipSobeID = 2,
                HotelID = 1
            };
        }

        public static ICollection<StavkaRacuna> GetStavke()
        {
            return new List<StavkaRacuna>
            {

                new StavkaRacuna{StavkaRacunaID=1, Kolicina=2,UslugaID=1,RacunID=1,},
                new StavkaRacuna{StavkaRacunaID=2,Kolicina=5,UslugaID=2 ,RacunID=2}
            };
        }

        public static StavkaRacuna GetInvalidStavka()
        {
            return new StavkaRacuna
            {
                Kolicina = 100,
                UslugaID = 2,
                RacunID = 1,
            };
        }

        public static StavkaRacuna GetValidStavka()
        {
            return new StavkaRacuna
            {
                StavkaRacunaID = 2,
                Kolicina = 3,
                UslugaID = 2,
                RacunID = 1
            };
        }

        public static ICollection<Usluga> GetUsluge()
        {
            return new List<Usluga>
            {

                new Usluga{UslugaID=1,CijenaUsluge=100,ImeUsluge="Vino"},
                new Usluga{UslugaID=2,CijenaUsluge=200,ImeUsluge="Masaza"}
            };
        }

        public static Usluga GetInvalidUsluga()
        {
            return new Usluga
            {
                UslugaID = 200,
                ImeUsluge = "Test",
            };
        }

        public static Usluga GetValidUsluga()
        {
            return new Usluga
            {
                UslugaID = 2,
                CijenaUsluge=200,
                ImeUsluge="Masaza"
            };
        }

        public static ICollection<Racun> GetRacuni()
        {
            return new List<Racun>
            {

                new Racun{RacunID=1,RezervacijaID=1,Placeno=true,IznosUkupno=1000},
                new Racun{RacunID=2,RezervacijaID=2,Placeno=true,IznosUkupno=2000}
            };
        }

        public static Racun GetInvalidRacun()
        {
            return new Racun
            {
                RacunID = 100,
                Placeno = true,
                IznosUkupno = 1000
            };
        }

        public static Racun GetValidRacun()
        {
            return new Racun
            {
                RacunID = 1,
                RezervacijaID = 1,
                Placeno = true,
                IznosUkupno = 1000
            };
        }

        public static ICollection<TipSobe> GetTipSoba()
        {
            return new List<TipSobe>
            {

                new TipSobe{TipSobeID=1,OpisSobe="test",CijenaPoNoci=100},
                new TipSobe{TipSobeID=2,OpisSobe="test1",CijenaPoNoci=200}
            };
        }

        public static TipSobe GetInvalidTipSobe()
        {
            return new TipSobe
            {
                TipSobeID = 1,
                OpisSobe = "test",
            };
        }

        public static TipSobe GetValidTipSobe()
        {
            return new TipSobe
            {
                TipSobeID = 1,
                OpisSobe = "test1",
                CijenaPoNoci = 1000
            };
        }

        public static ICollection<Rezervacija> GetRezervacije()
        {
            return new List<Rezervacija>
            {
                new Rezervacija 
                {
                    RezervacijaID=1,
                    GostID=1,
                    SobaID=1,
                    Prijava=new DateTime(2020,1,1),
                    Odjava=new DateTime(2020,2,2),
                    Popust=10,
                    Rezervirano=true,
                },
                new Rezervacija
                {
                    RezervacijaID=2,
                    GostID=2,
                    SobaID=2,
                    Prijava=new DateTime(2021,1,1),
                    Odjava=new DateTime(2021,2,2),
                    Popust=20,
                    Rezervirano=true,
                },
            };
        }

        public static Rezervacija GetInvalidRezervacija()
        {
            return new Rezervacija
            {
                RezervacijaID = 3,
                SobaID = 3,
                Prijava = new DateTime(2021, 1, 1),
                Odjava = new DateTime(2021, 2, 2),
                Popust = 20,
                Rezervirano = true,
            };
        }

        public static Rezervacija GetValidRezervacija()
        {
            return new Rezervacija
            {
                RezervacijaID = 2,
                GostID=2,
                SobaID = 2,
                Prijava = new DateTime(2021, 1, 1),
                Odjava = new DateTime(2021, 2, 2),
                Popust = 20,
                Rezervirano = true,
            };
        }

         public static RezervacijaView GetValidRezervacijaView()
         {
             return new RezervacijaView
             {
                 RezervacijaID = 1,
                 GostID = 1,
                 SobaID = 1,
                 Popust = 10,
                 Rezervirano = true,
                 Prijava = new DateTime(2020, 1, 1),
                 Odjava = new DateTime(2020, 2, 2)
             };
         }

        public static RezervacijaView GetInvalidRezervacijaView()
        {
            return new RezervacijaView
            {
                RezervacijaID = 1,
                GostID = 1,
                SobaID = 1,
                Popust = 10,
                Rezervirano = true,
                Prijava = new DateTime(2020, 2, 1),
                Odjava = new DateTime(2020, 1, 2)
            };
        }

        public static UslugeViewModel GetValidUslugaViewModel()
        {
            return new UslugeViewModel
            {
            CijenaRezervacije=1000,
            Prijava=new DateTime(2021,1, 1),
            Odjava = new DateTime(2021, 2, 2),
            Prezime="Test1",
            RezervacijaID=1,
            Popust=10,
            };
        }

        public static UslugeViewModel GetInvalidUslugaViewModel()
        {
            return new UslugeViewModel
            { };
        }
    }
}
