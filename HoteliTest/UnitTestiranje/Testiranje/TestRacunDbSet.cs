using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestiranje.Testiranje
{
    class TestRacunDbSet : TestDbSet<Racun>
    {
        public override Racun Find(params object[] keyValues)
        {
            return this.SingleOrDefault(reservation => reservation.RacunID == (int)keyValues.Single());
        }
    }
}
