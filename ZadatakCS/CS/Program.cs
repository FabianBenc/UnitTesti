using System;
using System.Collections.Generic;
using System.Linq;

namespace ZadatakCS
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Vozac> vozaci = new List<Vozac>()
            {
                new Vozac { Ime = "Fabian", Prezime = "Benc" },
                new Vozac { Ime = "Ivan", Prezime = "Ivic" },
                new Vozac { Ime = "Marko", Prezime = "Maric" },
                new Vozac { Ime = "Ivica", Prezime = "Novak" },
                new Vozac { Ime = "Mateo", Prezime = "Matic" },
            };

            List<Vozilo> vozila = new List<Vozilo>()
            {
                new Helikopter { NazivVozila = "Apache" },
                new Tenk { NazivVozila = "Sherman" },
                new Zrakoplov {NazivVozila= "F-16" },
                new Tenk {NazivVozila = "Abrams" },
                new Zrakoplov { NazivVozila = "Boeing" },
                
             };

            for (int i = 0; i < vozaci.Count; i++)
            {
                vozaci[i].DodajVozilo(vozila[i]);
            }
            Console.Write("\n OOP implementacija\n");
            Console.Write("1-Ispis svih vozaca\n2-Ispis vozaca koji voze terenska vozila\n3-Ispis vozaca koji voze letjelice\n4-Ispis svih vozila\n5-Ispis vozila koja su terenska vozila\n6-Ispis vozila koja su letjelice\n7-Izlaz.\n");
            Console.Write("Vas izbor: ");

            string IzborKorisnika = Console.ReadLine();

            switch (IzborKorisnika)
            {
                case "1":
                    Ispis(vozaci);
                    break;
                case "2":
                    var vozaciter = vozaci.Where(vozac => (vozac as Vozac).VoziTerenskaVozila());
                    Ispis(vozaciter);
                    //Ispis vozaca koji voze terenska vozila
                    break;
                case "3":
                    var vozacilet = vozaci.Where(vozac => (vozac as Vozac).VoziLetecaVozila());
                    Ispis(vozacilet);
                    //Ispis vozaca koji voze letjelice
                    break;
                case "4":
                    Ispis(vozila);
                    break;
                case "5":
                    var tervozilo = vozila.Where(l => l is ITerenskaVozila);
                    Ispis(tervozilo);
                    //Ispis vozila koja su terenska vozila
                    break;
                case "6":
                    var letvozilo = vozila.Where(l => l is ILetecaVozila);
                    Ispis(letvozilo);
                    //Ispis vozila koja su letjelice
                    break;
                case "7":
                    break;

                default:
                    break;
            }
        }

        public static void Ispis<T>(IEnumerable<T> popisvoz)
        {
            foreach (var x in popisvoz)
            {
                Console.WriteLine(x.ToString());
            }
            Console.WriteLine();
        }

        
    }
}
