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
    }
}
