using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestiranje.Testiranje
{
    class TestSobaDbSet : TestDbSet<Soba>
    {
        public override Soba Find(params object[] keyValues)
        {
            return this.SingleOrDefault(reservation => reservation.SobaID == (int)keyValues.Single());
        }
    }
}
