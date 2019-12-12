using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestiranje.Testiranje
{
    class TestStavkaRacunaDbSet : TestDbSet<StavkaRacuna>
    {
        public override StavkaRacuna Find(params object[] keyValues)
        {
            return this.SingleOrDefault(reservation => reservation.StavkaRacunaID == (int)keyValues.Single());
        }
    }
}
