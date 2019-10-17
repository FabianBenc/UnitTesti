using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ZadatakCS
{
    class Vozac
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }

        List<Vozilo> Vozila = new List<Vozilo>();

        public void DodajVozilo(Vozilo vozilo)
        {
            Vozila.Add(vozilo);
        }

        public override string ToString()
        {
            return $"{nameof(Ime)}: {Ime}\n{nameof(Prezime)}: {Prezime}";
        }

        public bool VoziTerenskaVozila()
        {
            return Vozila.Any(x => x is ITerenskaVozila);
        }

        public bool VoziLetecaVozila()
        {
            return Vozila.Any(x => x is ILetecaVozila);
        }
    }
}
