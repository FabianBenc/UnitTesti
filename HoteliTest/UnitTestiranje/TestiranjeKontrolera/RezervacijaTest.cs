using HoteliTest.Controllers;
using HoteliTest.Models;
using HoteliTest.ViewModels;
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
    public class RezervacijaTest
    {
        private Mock<IHotelAC> mockContext;
        private Mock<DbSet<Rezervacija>> mockSet;

        [TestMethod]
        public void Create_ShouldCreateReservation()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Rezervacija>>();
            mockContext.Setup(m => m.Rezervacije).Returns(mockSet.Object);

            var controller = new RezervacijaController(mockContext.Object);
            controller.Create(new RezervacijaView());

            mockSet.Verify(m => m.Add(It.IsAny<Rezervacija>()), Times.Once, "Could not add the reservation to the database");
            mockContext.Verify(m => m.SaveChanges(), Times.Once, "Could not save the changes to the database");
        }

        [TestMethod]
        public void PickDate_ShouldRedirectToPickRoom()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Rezervacija>>();
            mockContext.Setup(m => m.Rezervacije).Returns(mockSet.Object);

            var controller = new RezervacijaController(mockContext.Object);
            var result = controller.OdabirDatuma(new RezervacijaView()) as RedirectToRouteResult;

            Assert.AreEqual("OdabirSobe", result.RouteValues["action"], "The pick room route does not match the controllers route");
            Assert.IsNull(result.RouteValues["RezervacijaController"], "The reservation controller is not null");
        }

        [TestMethod]
        public void Create_ShouldRedirectToIndex()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Rezervacija>>();
            mockContext.Setup(m => m.Rezervacije).Returns(mockSet.Object);

            var controller = new RezervacijaController(mockContext.Object);
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(new RezervacijaView());

            Assert.AreEqual("Index", result.RouteValues["action"], "The index route does not match the controllers route");
            Assert.IsNull(result.RouteValues["RezervacijaController"], "The reservation controller is not null");
        }

        [TestMethod]
        public void Create_ShouldReturnInvalidModelState()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Rezervacija>>();
            mockContext.Setup(m => m.Rezervacije).Returns(mockSet.Object);

            var controller = new RezervacijaController(mockContext.Object);
            controller.ModelState.AddModelError("SobaID", "Room id must contain a value");

            var newReservation = new RezervacijaView()
            {
                RezervacijaID = 1,
                Prijava = DateTime.Now,
                Odjava = DateTime.Now,
                Popust = 0,
                GostID = 1,
                Rezervirano = true,
                SobaID = -1
            };
            controller.Create(newReservation);

            Assert.IsNotNull(newReservation, "There is no data in the reservation");
            Assert.IsTrue(controller.ViewData.ModelState.IsValid == false, "Room id is required");
        }

        [TestMethod]
        public void Update_ShouldUpdateReservation()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Rezervacija>>();
            mockContext.Setup(m => m.Rezervacije).Returns(mockSet.Object);

            var controller = new RezervacijaController(mockContext.Object);
            var reservation = MockingReservation();

            controller.Create(reservation);
            reservation.BrojSobe = 400;
            controller.Edit(2);

            Assert.IsTrue(400 == reservation.BrojSobe, "The reservation did not update");
        }

        [TestMethod]
        public void Delete_ShouldReturnNotFound()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Rezervacija>>();
            mockContext.Setup(m => m.Rezervacije).Returns(mockSet.Object);

            var controller = new RezervacijaController(mockContext.Object);
            controller.Create(new RezervacijaView { RezervacijaID = 1 });
            controller.Delete(2);
            controller.DeleteConfirmed(2);
            ActionResult result = controller.Delete(2);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult), "Reservation does not exist");
        }

        [TestMethod]
        public void Index_ShouldReturnSameCountOfReservations()
        {
            var context = new TestHotelContext();

            var controller = new RezervacijaController(context);
            controller.Create(new RezervacijaView());
            controller.Create(new RezervacijaView());
            controller.Create(new RezervacijaView());
            var result = controller.Index() as ViewResult;
            var guestData = (IList<Rezervacija>)result.ViewData.Model;

            Assert.IsNotNull(guestData, "There's no reservation data");
            Assert.AreEqual(3, guestData.Count, "The count does not match");
        }

        [TestMethod]
        public void Details_ShouldReturnCorrectReservation()
        {
            mockSet = new Mock<DbSet<Rezervacija>>();
            mockContext = new Mock<IHotelAC>();
            mockContext.Setup(x => x.Rezervacije.Find(3)).Returns(new Rezervacija { RezervacijaID = 3 });

            var controller = new RezervacijaController(mockContext.Object);
            var result = controller.Details(3) as ViewResult;
            var reservation = (Rezervacija)result.ViewData.Model;

            Assert.IsNotNull(reservation.RezervacijaID, "Reservation Id is null");
            Assert.AreEqual(3, reservation.RezervacijaID, "The Id's are not equal");
        }

        public static Soba MockingRoom()
        {
            var room = new Soba
            {
                HotelID = 1,
                SobaID = 2,
                BrojSobe = 100,
                TipSobeID = 1
            };
            return room;
        }
        public static Gost MockingGuest()
        {
            var guest = new Gost
            {
                Adresa = "Test",
                Email = "Test",
                Ime = "Test",
                Prezime = "Test",
                GostID = 1
            };
            return guest;
        }

        public static RezervacijaView MockingReservation()
        {
            var reservation = new RezervacijaView()
            {
                Prijava = DateTime.Parse("20-11-2019"),
                Odjava = DateTime.Parse("25-11-2019"),
                Popust = 0,
                GostID = 2,
                RezervacijaID = 2,
                SobaID = 2,
                Rezervirano = true
            };
            return reservation;
        }
    }
}
