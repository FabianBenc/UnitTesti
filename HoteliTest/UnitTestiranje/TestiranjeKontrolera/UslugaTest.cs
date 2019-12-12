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
    public class UslugaTest
    {
        private Mock<IHotelAC> mockContext;
        private Mock<DbSet<Usluga>> mockSet;

        [TestMethod]
        public void Create_ShouldCreateService()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Usluga>>();
            mockContext.Setup(m => m.Usluge).Returns(mockSet.Object);

            var controller = new UslugaController(mockContext.Object);
            controller.Create(new Usluga());

            mockSet.Verify(m => m.Add(It.IsAny<Usluga>()), Times.Once, "Could not add the service to the database");
            mockContext.Verify(m => m.SaveChanges(), Times.Once, "Could not save the changes to the database");
        }

        [TestMethod]
        public void Create_ShouldRedirect()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Usluga>>();
            mockContext.Setup(m => m.Usluge).Returns(mockSet.Object);

            var controller = new UslugaController(mockContext.Object);
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(new Usluga());

            Assert.AreEqual("Index", result.RouteValues["action"], "The index route does not match the controllers route");
            Assert.IsNull(result.RouteValues["UslugaController"], "The room type controller is not null");
        }

        [TestMethod]
        public void Delete_ShouldDeleteService()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Usluga>>();
            mockContext.Setup(m => m.Usluge).Returns(mockSet.Object);
            mockSet.Setup(m => m.Remove(It.IsAny<Usluga>()));

            var controller = new UslugaController(mockContext.Object);
            controller.Create(new Usluga() { UslugaID = 1 });
            controller.Delete(1);
            controller.DeleteConfirmed(1);

            mockSet.Verify(m => m.Remove(It.IsAny<Usluga>()), Times.Once, "Could not remove the service");
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(2), "Could not save the changes to the database");
        }

        [TestMethod]
        public void Create_ShouldReturnInvalidModelState()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Usluga>>();
            mockContext.Setup(m => m.Usluge).Returns(mockSet.Object);

            var controller = new UslugaController(mockContext.Object);
            controller.ModelState.AddModelError("CijenaUsluge", "Service price needs to have a value");

            var service = new Usluga
            {
                ImeUsluge = "test",
                UslugaID = 1,
                CijenaUsluge = 0
            };
            controller.Create(service);

            Assert.IsTrue(controller.ViewData.ModelState.IsValid == false, "Service price cannot be zero");
        }

        [TestMethod]
        public void Update_ShouldUpdateService()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Usluga>>();
            mockContext.Setup(m => m.Usluge).Returns(mockSet.Object);

            var controller = new UslugaController(mockContext.Object);
            var service = new Usluga
            {
                ImeUsluge = "test",
                UslugaID = 1,
                CijenaUsluge = 10
            };

            controller.Create(service);
            service.CijenaUsluge = 20;
            controller.Edit(service);

            Assert.IsTrue(20 == service.CijenaUsluge, "The service did not update");
        }

        [TestMethod]
        public void Delete_ShouldReturnNotFound()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Usluga>>();
            mockContext.Setup(m => m.Usluge).Returns(mockSet.Object);

            var controller = new UslugaController(mockContext.Object);
            controller.Create(new Usluga { UslugaID = 1 });
            controller.Delete(2);
            controller.DeleteConfirmed(2);
            ActionResult result = controller.Delete(2);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult), "Service does not exist");
        }

        [TestMethod]
        public void Index_ShouldReturnSameCountOfService()
        {
            var context = new TestHotelContext();

            var controller = new UslugaController(context);
            controller.Create(new Usluga());
            controller.Create(new Usluga());
            controller.Create(new Usluga());
            var result = controller.Index() as ViewResult;
            var service = (IList<Usluga>)result.ViewData.Model;

            Assert.IsNotNull(service, "There's no service data");
            Assert.AreEqual(3, service.Count, "The count does not match");
        }

        [TestMethod]
        public void Details_ShouldReturnCorrectService()
        {
            mockSet = new Mock<DbSet<Usluga>>();
            mockContext = new Mock<IHotelAC>();
            mockContext.Setup(x => x.Usluge.Find(12)).Returns(new Usluga() { UslugaID = 12 });

            var controller = new UslugaController(mockContext.Object);
            var result = controller.Details(12) as ViewResult;
            var service = (Usluga)result.ViewData.Model;

            Assert.IsNotNull(service.UslugaID, "Service id is null");
            Assert.AreEqual(12, service.UslugaID, "The Id's are not equal");
        }
    }
}
