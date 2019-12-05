using HoteliTest.DAL;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitT3st.Controllers
{
    public class BaseControllerUTest
    {
        protected static Mock<HotelContext> testContext = TestInicijalizacija.context;
        protected const int nepostojeciID = -1;
        protected const int postojeciID = 1;
        protected const int brisaniID = 2;
    }
}
