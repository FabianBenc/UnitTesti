using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestiranje.Testiranje
{
    class TestGostDbSet : TestDbSet<Gost>
    {
        public override Gost Find(params object[] keyValues)
        {
            return this.SingleOrDefault(reservation => reservation.GostID == (int)keyValues.Single());
        }
    }
}
