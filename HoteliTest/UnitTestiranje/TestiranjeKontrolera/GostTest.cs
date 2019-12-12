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
    public class GostTest
    {
        private Mock<IHotelAC> mockContext;
        private Mock<DbSet<Gost>> mockSet;

        [TestMethod]
        public void Create_ShouldCreateGuest()
        {

            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Gost>>();
            mockContext.Setup(m => m.Gosti).Returns(mockSet.Object);

            var controller = new GostController(mockContext.Object);
            controller.Create(new Gost());

            mockSet.Verify(m => m.Add(It.IsAny<Gost>()), Times.Once, "Could not add the guest to the database");
            mockContext.Verify(m => m.SaveChanges(), Times.Once, "Could not save the changes to the database");
        }

        [TestMethod]
        public void Create_ShouldRedirect()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Gost>>();
            mockContext.Setup(m => m.Gosti).Returns(mockSet.Object);

            var controller = new GostController(mockContext.Object);
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(new Gost());

            Assert.AreEqual("Index", result.RouteValues["action"], "The index route does not match the controllers route");
            Assert.IsNull(result.RouteValues["GostController"], "The guest controller is not null");
        }

        [TestMethod]
        public void Delete_ShouldDeleteGuest()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Gost>>();
            mockContext.Setup(m => m.Gosti).Returns(mockSet.Object);
            mockSet.Setup(m => m.Remove(It.IsAny<Gost>()));
            var controller = new GostController(mockContext.Object);
            controller.Create(new Gost { GostID = 1 });
            controller.Delete(1);
            controller.DeleteConfirmed(1);

            mockSet.Verify(m => m.Remove(It.IsAny<Gost>()), Times.Once, "Could not remove the guest");
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(2), "Could not save the changes to the database");
        }

        [TestMethod]
        public void Create_ShouldReturnInvalidModelState()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Gost>>();
            mockContext.Setup(m => m.Gosti).Returns(mockSet.Object);

            var controller = new GostController(mockContext.Object);
            controller.ModelState.AddModelError("LastName", "Last name cannot be empty");

            var newGuest = new Gost
            {
                Ime = "DD",
                Prezime = null,
                GostID = 1,
                Adresa = "SS",
                Email = "AA",
            };
            controller.Create(newGuest);

            Assert.IsTrue(controller.ViewData.ModelState.IsValid == false, "Last name is required");
        }

        [TestMethod]
        public void Update_ShouldUpdateGuest()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Gost>>();
            mockContext.Setup(m => m.Gosti).Returns(mockSet.Object);

            var controller = new GostController(mockContext.Object);
            var newGuest = new Gost
            {
                Ime = "AA",
                Prezime = "BB",
                GostID = 1,
                Adresa = "CC",
                Email = "SS",
            };

            controller.Create(newGuest);
            newGuest.Ime = "SS";
            controller.Edit(newGuest);

            Assert.IsTrue("SS" == newGuest.Ime, "The guest did not update");
        }

        [TestMethod]
        public void Delete_ShouldReturnNotFound()
        {

            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Gost>>();
            mockContext.Setup(m => m.Gosti).Returns(mockSet.Object);

            var controller = new GostController(mockContext.Object);
            controller.Create(new Gost { GostID = 1 });
            controller.Delete(2);
            controller.DeleteConfirmed(2);
            ActionResult result = controller.Delete(2);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult), "Guest does not exist");
        }

        [TestMethod]
        public void Index_ShouldReturnSameCountOfGuests()
        {
            var context = new TestHotelContext();

            var controller = new GostController(context);
            controller.Create(new Gost());
            controller.Create(new Gost());
            controller.Create(new Gost());
            var result = controller.Index() as ViewResult;
            var guestData = (IList<Gost>)result.ViewData.Model;

            Assert.IsNotNull(guestData, "There's no guest data");
            Assert.AreEqual(3, guestData.Count, "The count does not match");
        }

        [TestMethod]
        public void Details_ShouldReturnCorrectGuest()
        {
            mockSet = new Mock<DbSet<Gost>>();
            mockContext = new Mock<IHotelAC>();
            mockContext.Setup(x => x.Gosti.Find(21)).Returns(new Gost { GostID = 21 });

            var controller = new GostController(mockContext.Object);
            var result = controller.Details(21) as ViewResult;
            var guest = (Gost)result.ViewData.Model;

            Assert.IsNotNull(guest.GostID, "Guest Id is null");
            Assert.AreEqual(21, guest.GostID, "The Id's are not equal");
        }
    }
}
