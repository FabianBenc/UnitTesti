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
    public class TipSobeTest
    {
        private Mock<IHotelAC> mockContext;
        private Mock<DbSet<TipSobe>> mockSet;

        [TestMethod]
        public void Create_ShouldCreateRoomType()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<TipSobe>>();
            mockContext.Setup(m => m.TipSoba).Returns(mockSet.Object);

            var controller = new TipSobeController(mockContext.Object);
            controller.Create(new TipSobe());

            mockSet.Verify(m => m.Add(It.IsAny<TipSobe>()), Times.Once, "Could not add the room type to the database");
            mockContext.Verify(m => m.SaveChanges(), Times.Once, "Could not save the changes to the database");
        }

        [TestMethod]
        public void Create_ShouldRedirect()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<TipSobe>>();
            mockContext.Setup(m => m.TipSoba).Returns(mockSet.Object);

            var controller = new TipSobeController(mockContext.Object);
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(new TipSobe());

            Assert.AreEqual("Index", result.RouteValues["action"], "The index route does not match the controllers route");
            Assert.IsNull(result.RouteValues["TipSobeController"], "The room type controller is not null");
        }

        [TestMethod]
        public void Delete_ShouldDeleteRoomType()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<TipSobe>>();
            mockContext.Setup(m => m.TipSoba).Returns(mockSet.Object);
            mockSet.Setup(m => m.Remove(It.IsAny<TipSobe>()));

            var controller = new TipSobeController(mockContext.Object);
            controller.Create(new TipSobe { TipSobeID = 1 });
            controller.Delete(1);
            controller.DeleteConfirmed(1);

            mockSet.Verify(m => m.Remove(It.IsAny<TipSobe>()), Times.Once, "Could not remove the room type");
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(2), "Could not save the changes to the database");
        }

        [TestMethod]
        public void Create_ShouldReturnInvalidModelState()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<TipSobe>>();
            mockContext.Setup(m => m.TipSoba).Returns(mockSet.Object);

            var controller = new TipSobeController(mockContext.Object);
            controller.ModelState.AddModelError("CijenaSobe", "Room price needs to have a value");

            var newRoomType = new TipSobe
            {
                CijenaPoNoci = 0,
                OpisSobe = "Test",
                TipSobeID = 1
            };
            controller.Create(newRoomType);

            Assert.IsTrue(controller.ViewData.ModelState.IsValid == false, "Room price cannot be zero");
        }

        [TestMethod]
        public void Update_ShouldUpdateRoomType()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<TipSobe>>();
            mockContext.Setup(m => m.TipSoba).Returns(mockSet.Object);

            var controller = new TipSobeController(mockContext.Object);
            var newRoomType = new TipSobe
            {
                CijenaPoNoci = 100,
                OpisSobe = "Test",
                TipSobeID = 1
            };

            controller.Create(newRoomType);
            newRoomType.CijenaPoNoci = 200;
            controller.Edit(newRoomType);

            Assert.IsTrue(200 == newRoomType.CijenaPoNoci, "The room type did not update");
        }

        [TestMethod]
        public void Delete_ShouldReturnNotFound()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<TipSobe>>();
            mockContext.Setup(m => m.TipSoba).Returns(mockSet.Object);

            var controller = new TipSobeController(mockContext.Object);
            controller.Create(new TipSobe { TipSobeID = 1 });
            controller.Delete(2);
            controller.DeleteConfirmed(2);
            ActionResult result = controller.Delete(2);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult), "Room type does not exist");
        }

        [TestMethod]
        public void Index_ShouldReturnSameCountOfRoomTypes()
        {
            var context = new TestHotelContext();

            var controller = new TipSobeController(context);
            controller.Create(new TipSobe());
            controller.Create(new TipSobe());
            controller.Create(new TipSobe());
            var result = controller.Index() as ViewResult;
            var roomTypeData = (IList<TipSobe>)result.ViewData.Model;

            Assert.IsNotNull(roomTypeData, "There's no room type data");
            Assert.AreEqual(3, roomTypeData.Count, "The count does not match");
        }

        [TestMethod]
        public void Details_ShouldReturnCorrectRoomType()
        {
            mockSet = new Mock<DbSet<TipSobe>>();
            mockContext = new Mock<IHotelAC>();
            mockContext.Setup(x => x.TipSoba.Find(12)).Returns(new TipSobe { TipSobeID = 12 });

            var controller = new TipSobeController(mockContext.Object);
            var result = controller.Details(12) as ViewResult;
            var roomType = (TipSobe)result.ViewData.Model;

            Assert.IsNotNull(roomType.TipSobeID, "Room type Id is null");
            Assert.AreEqual(12, roomType.TipSobeID, "The Id's are not equal");
        }
    }
}
