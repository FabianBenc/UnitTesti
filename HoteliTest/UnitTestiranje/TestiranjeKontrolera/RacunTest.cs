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
    public class RacunTest
    {
        private int ID = 1;
        private Mock<IHotelAC> mockContext;
        private Mock<DbSet<Racun>> mockSet;

        [TestMethod]
        public void Create_ShouldCreateReceipt()
        {

            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Racun>>();
            mockContext.Setup(m => m.Racuni).Returns(mockSet.Object);

            var controller = new RacunController(mockContext.Object);
            controller.Create(new Racun(), new Rezervacija(), ID);

            mockSet.Verify(m => m.Add(It.IsAny<Racun>()), Times.Once, "Could not add the guest to the database");
            mockContext.Verify(m => m.SaveChanges(), Times.Once, "Could not save the changes to the database");
        }

        [TestMethod]
        public void Create_ShouldRedirect()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Racun>>();
            mockContext.Setup(m => m.Racuni).Returns(mockSet.Object);

            var controller = new RacunController(mockContext.Object);
            RedirectToRouteResult result =
                (RedirectToRouteResult)controller.Create(new Racun(), new Rezervacija(), ID);

            Assert.AreEqual("Index", result.RouteValues["action"],
                "The index route does not match the controllers route");
            Assert.IsNull(result.RouteValues["RacunController"], "The receipts controller is not null");
        }

        [TestMethod]
        public void Delete_ShouldDeleteReceipt()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Racun>>();
            mockContext.Setup(m => m.Racuni).Returns(mockSet.Object);
            mockSet.Setup(m => m.Remove(It.IsAny<Racun>()));
            var controller = new RacunController(mockContext.Object);
            controller.Create(new Racun { RacunID = 1 }, new Rezervacija(), ID);
            controller.Delete(1);
            controller.DeleteConfirmed(1);

            mockSet.Verify(m => m.Remove(It.IsAny<Racun>()), Times.Once, "Could not remove the receipt");
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(2), "Could not save the changes to the database");
        }

        [TestMethod]
        public void Create_ShouldReturnInvalidModelState()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Racun>>();
            mockContext.Setup(m => m.Racuni).Returns(mockSet.Object);

            var controller = new RacunController(mockContext.Object);
            controller.ModelState.AddModelError("RezervacijaID", "Reservation id must contain a valid value");

            var newReceipt = new Racun()
            {
                RacunID = 1,
                IznosUkupno = 0,
                Placeno = false,
                RezervacijaID = -1,
            };
            controller.Create(newReceipt, new Rezervacija(), ID);

            Assert.IsNotNull(newReceipt, "There is no data in the receipts");
            Assert.IsTrue(controller.ViewData.ModelState.IsValid == false, "Reservation id is required");
        }

        [TestMethod]
        public void Update_ShouldUpdateReceipt()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Racun>>();
            mockContext.Setup(m => m.Racuni).Returns(mockSet.Object);

            var controller = new RacunController(mockContext.Object);
            var newReceipt = new Racun()
            {
                RacunID = 1,
                IznosUkupno = 0,
                Placeno = false,
                RezervacijaID = -1,
            };

            controller.Create(newReceipt, new Rezervacija(), ID);
            newReceipt.Placeno = true;
            controller.Edit(newReceipt);

            Assert.IsTrue(true == newReceipt.Placeno, "The receipt did not update");
        }

        [TestMethod]
        public void Delete_ShouldReturnNotFound()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Racun>>();
            mockContext.Setup(m => m.Racuni).Returns(mockSet.Object);

            var controller = new RacunController(mockContext.Object);
            controller.Create(new Racun() { RacunID = 1 }, new Rezervacija(), ID);
            controller.Delete(2);
            controller.DeleteConfirmed(2);
            ActionResult result = controller.Delete(2);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult), "Guest does not exist");
        }

        [TestMethod]
        public void Index_ShouldReturnSameCountOfGuests()
        {
            var context = new TestHotelContext();

            var controller = new RacunController(context);
            controller.Create(new Racun(), new Rezervacija(), ID);
            controller.Create(new Racun(), new Rezervacija(), ID);
            controller.Create(new Racun(), new Rezervacija(), ID);
            var result = controller.Index() as ViewResult;
            var receiptData = (IList<Racun>)result.ViewData.Model;

            Assert.IsNotNull(receiptData, "There's no receipt data");
            Assert.AreEqual(3, receiptData.Count, "The count does not match");
        }

        [TestMethod]
        public void Details_ShouldReturnCorrectReceipt()
        {
            var context = new TestHotelContext();
            context.Racuni.Add(new Racun { });
            context.StavkeRacuna.Add(mockingAccountItem());
            context.Usluge.Add(mockingService());

            var controller = new RacunController(context);
            var result = controller.Details(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IQueryable<StavkaRacuna>));
        }

        public static Usluga mockingService()
        {
            var service = new Usluga()
            {
                ImeUsluge = "Test",
                UslugaID = 1,
                CijenaUsluge = 10
            };
            return service;
        }

        public static StavkaRacuna mockingAccountItem()
        {
            var accountItem = new StavkaRacuna()
            {
                StavkaRacunaID = 1,
                UslugaID = 1
            };
            return accountItem;
        }

        public static UslugeViewModel MockingViewModel()
        {
            var serviceview = new UslugeViewModel()
            {
                Prijava = DateTime.Parse("20-11-2019"),
                Odjava = DateTime.Parse("25-11-2019"),
                Popust = 0,
                RezervacijaID = 2,
                CijenaRezervacije = 200,
                RacunID = 1
            };
            return serviceview;
        }

        public static Soba roomMock()
        {
            var newRoom = new Soba
            {
                BrojSobe = 100,
                HotelID = 1,
                SobaID = 1,
                TipSobeID = 1
            };
            return newRoom;
        }

        public static Rezervacija mockingReservation()
        {
            var reservation = new Rezervacija()
            {
                Prijava = DateTime.Parse("20-11-2019"),
                Odjava = DateTime.Parse("25-11-2019"),
                Popust = 0,
                GostID = 1,
                RezervacijaID = 1,
                SobaID = 1,
                Rezervirano = true,
            };
            return reservation;
        }
    }
}
