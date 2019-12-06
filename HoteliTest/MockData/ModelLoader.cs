using HoteliTest.Models;
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
    }
}
