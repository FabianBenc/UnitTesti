using HoteliTest.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitT3st
{
    [TestClass]
    public class TestInicijalizacija
    {
        public static Mock<HotelContext> context { get; set; }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext tc)
        {
            context = MockHotel_Data.Create();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            MockHotel_Data.Dispose(context);
        }
    }
}
