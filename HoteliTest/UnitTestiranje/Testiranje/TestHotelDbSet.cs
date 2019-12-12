using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestiranje.Testiranje
{
    class TestHotelDbSet : TestDbSet<Hotel>
    {
        public override Hotel Find(params object[] keyValues)
        {
            return this.SingleOrDefault(reservation => reservation.HotelID == (int)keyValues.Single());
        }
    }
}
