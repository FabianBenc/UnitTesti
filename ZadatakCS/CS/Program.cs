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

            Program meni = new Program();

            Console.Write("\n OOP implementacija\n");
            Console.Write("1-Ispis svih vozaca\n2-Ispis vozaca koji voze terenska vozila\n3-Ispis vozaca koji voze letjelice\n4-Ispis svih vozila\n5-Ispis vozila koja su terenska vozila\n6-Ispis vozila koja su letjelice\n7-Izlaz.\n");
            Console.Write("Vas izbor: ");

            string IzborKorisnika = Console.ReadLine();

            switch (IzborKorisnika)
            {
                case "1":

                    foreach (Vozac a in vozaci)
                    {
                        Console.WriteLine(a);
                    }
                    break;
                case "2":
                    var VozaciTerenskihVozila = from vozac in vozaci where vozac.VoziTerenskaVozila() select vozac;
                    foreach (Vozac b in VozaciTerenskihVozila)
                    {
                        Console.WriteLine(b);
                    }
                    //Ispis vozaca koji voze terenska vozila
                    break;
                case "3":
                    var VozaciLetecihVozila = from vozac in vozaci where vozac.VoziLetecaVozila() select vozac;
                    foreach (Vozac c in VozaciLetecihVozila)
                    {
                        Console.WriteLine(c);
                    }
                    //Ispis vozaca koji voze letjelice
                    break;
                case "4":
                    foreach (Vozilo b in vozila)
                    {
                        Console.WriteLine(b);
                    }
                    break;
                case "5":
                    var TerenskaVozila = from vozilo in vozila where vozilo is ITerenskaVozila select vozilo;
                    foreach (Vozilo c in TerenskaVozila)
                    {
                        Console.WriteLine(c);
                    }
                    //Ispis vozila koja su terenska vozila
                    break;
                case "6":
                    var LetecaVozila = from vozilo in vozila where vozilo is ILetecaVozila select vozilo;
                    foreach (Vozilo c in LetecaVozila)
                    {
                        Console.WriteLine(c);
                    }
                    //Ispis vozila koja su letjelice
                    break;
                case "7":
                    break;

                default:
                    break;
            }
        }
    }
}
