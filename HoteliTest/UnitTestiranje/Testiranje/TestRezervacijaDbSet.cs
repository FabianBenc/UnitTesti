using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestiranje.Testiranje
{
    class TestRezervacijaDbSet : TestDbSet<Rezervacija>
    {
        public override Rezervacija Find(params object[] keyValues)
        {
            return this.SingleOrDefault(reservation => reservation.RezervacijaID == (int)keyValues.Single());
        }
    }
}
