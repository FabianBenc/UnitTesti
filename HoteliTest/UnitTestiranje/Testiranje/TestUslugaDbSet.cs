using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestiranje.Testiranje
{
    class TestUslugaDbSet : TestDbSet<Usluga>
    {
        public override Usluga Find(params object[] keyValues)
        {
            return this.SingleOrDefault(reservation => reservation.UslugaID == (int)keyValues.Single());
        }
    }
}
