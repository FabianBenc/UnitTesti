using HoteliTest.Controllers;
using HoteliTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UnitTestiranje.Testiranje;

namespace UnitTestiranje.TestiranjeKontrolera
{
    [TestClass]
    public class HotelTest
    {

        private Mock<IHotelAC> mockContext;
        private Mock<DbSet<Hotel>> mockSet;

        [TestMethod]
        public void Create_ShouldCreateOneHotel()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Hotel>>();
            mockContext.Setup(m => m.Hoteli).Returns(mockSet.Object);

            var controller = new HotelController(mockContext.Object);
            controller.Create(new Hotel());

            mockSet.Verify(m => m.Add(It.IsAny<Hotel>()), Times.Once, "Could not add a hotel");
            mockContext.Verify(m => m.SaveChanges(), Times.Once, "Could not add the hotel to the database");
        }

        [TestMethod]
        public void Index_ShouldReturnSameCountOfHotels()
        {
            var context = new TestHotelContext();

            var controller = new HotelController(context);
            controller.Create(new Hotel());
            controller.Create(new Hotel());
            controller.Create(new Hotel());
            var result = controller.Index() as ViewResult;
            var hotelsData = (IList<Hotel>)result.ViewData.Model;

            Assert.IsNotNull(hotelsData, "There is no data in the hotels list");
            Assert.AreEqual(3, hotelsData.Count(), "The data does not corespond to the expected data count");
        }

        [TestMethod]
        public void Details_ShouldReturnCorrectHotel()
        {
            mockSet = new Mock<DbSet<Hotel>>();
            mockContext = new Mock<IHotelAC>();
            mockContext.Setup(x => x.Hoteli.Find(42)).Returns(new Hotel { HotelID = 42 });

            var controller = new HotelController(mockContext.Object);
            var result = controller.Details(42) as ViewResult;
            var hotel = (Hotel)result.ViewData.Model;

            Assert.IsNotNull(hotel.HotelID, "There does not exist a hotel");
            Assert.AreEqual(42, hotel.HotelID, "The expected id does not match the hotel id");
        }

        [TestMethod]
        public void Delete_ShouldDeleteHotel()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Hotel>>();
            mockContext.Setup(m => m.Hoteli).Returns(mockSet.Object);

            var controller = new HotelController(mockContext.Object);
            controller.Create(new Hotel { HotelID = 1 });
            controller.Delete(1);
            controller.DeleteConfirmed(1);

            mockSet.Verify(m => m.Remove(It.IsAny<Hotel>()), Times.Once, "There are no hotels");
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(2), "Could not remove the hotel from the database");
        }

        [TestMethod]
        public void Delete_ShouldReturnNotFound()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Hotel>>();
            mockContext.Setup(m => m.Hoteli).Returns(mockSet.Object);

            var controller = new HotelController(mockContext.Object);
            controller.Create(new Hotel { HotelID = 1 });
            controller.Delete(2);
            controller.DeleteConfirmed(2);
            ActionResult result = controller.Delete(2);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult), "Hotel does not exist");
        }

        [TestMethod]
        public void Update_ShouldUpdateHotel()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Hotel>>();
            mockContext.Setup(m => m.Hoteli).Returns(mockSet.Object);

            var controller = new HotelController(mockContext.Object);
            var newHotel = new Hotel
            {
                HotelID = 1,
                Adresa = "FF",
                Lokacija = "BB",
                Ime = "BB"
            };

            controller.Create(newHotel);
            newHotel.Ime = "OO";
            controller.Edit(newHotel);

            Assert.IsTrue("OO" == newHotel.Ime, "The hotel did not update");
        }

        [TestMethod]
        public void Create_ShouldReturnInvalidModelState()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Hotel>>();
            mockContext.Setup(m => m.Hoteli).Returns(mockSet.Object);

            var controller = new HotelController(mockContext.Object);
            controller.ModelState.AddModelError("Name", "Name cannot be null");

            var newHotel = new Hotel
            {
                Ime = null,
                Adresa = "AA",
                HotelID = 1,
                Lokacija = "BB"

            };
            controller.Create(newHotel);

            Assert.IsTrue(controller.ViewData.ModelState.IsValid == false, "Name is required");
        }
    }
}
