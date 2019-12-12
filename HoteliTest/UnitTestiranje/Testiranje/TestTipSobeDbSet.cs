using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestiranje.Testiranje
{
    class TestTipSobeDbSet : TestDbSet<TipSobe>
    {
        public override TipSobe Find(params object[] keyValues)
        {
            return this.SingleOrDefault(reservation => reservation.TipSobeID == (int)keyValues.Single());
        }
    }
}
