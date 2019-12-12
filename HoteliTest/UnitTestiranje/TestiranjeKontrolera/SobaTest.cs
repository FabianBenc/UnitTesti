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
    public class SobaTest
    {
        private Mock<IHotelAC> mockContext;
        private Mock<DbSet<Soba>> mockSet;

        [TestMethod]
        public void Create_ShouldCreateRoom()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Soba>>();
            mockContext.Setup(m => m.Sobe).Returns(mockSet.Object);

            var controller = new SobaController(mockContext.Object);
            controller.Create(new Soba());

            mockSet.Verify(m => m.Add(It.IsAny<Soba>()), Times.Once, "Could not create a room");
            mockContext.Verify(m => m.SaveChanges(), Times.Once, "Could not save the changes to the database");
        }

        [TestMethod]
        public void Create_ShouldNotCreateRoomWithoutRoomType()
        {
            var context = new TestHotelContext();

            Hotel p = new Hotel();
            TipSobe c = new TipSobe();
            context.Hoteli.Add(p);
            context.TipSoba.Add(c);

            var controller = new SobaController(context);
            controller.Create(new Soba { Hotel = p, TipSobe = c, TipSobeID = 5 });
            controller.ModelState.AddModelError("TipSobeID", "Room type does not exist");

            Assert.IsTrue(controller.ModelState.IsValid == false, "The model state is valid");
        }

        [TestMethod]
        public void Create_ShouldRedirectToIndex()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Soba>>();
            mockContext.Setup(m => m.Sobe).Returns(mockSet.Object);

            var controller = new SobaController(mockContext.Object);
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(new Soba());

            Assert.AreEqual("Index", result.RouteValues["action"], "The specified route doesnt redirect to index");
            Assert.IsNull(result.RouteValues["SobaController"], "The route in the rooms controller is pointing to null");
        }

        [TestMethod]
        public void Delete_ShouldDeleteCorrectRoom()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Soba>>();
            mockContext.Setup(m => m.Sobe).Returns(mockSet.Object);

            var controller = new SobaController(mockContext.Object);
            controller.Create(new Soba { SobaID = 10 });
            controller.Delete(10);
            controller.DeleteConfirmed(10);

            mockSet.Verify(m => m.Remove(It.IsAny<Soba>()), Times.Once, "Room failed to remove");
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(2), "Could not save changes");
        }

        [TestMethod]
        public void Delete_ShouldReturnNotFound()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Soba>>();
            mockContext.Setup(m => m.Sobe).Returns(mockSet.Object);

            var controller = new SobaController(mockContext.Object);
            controller.Create(new Soba { SobaID = 1 });
            controller.Delete(2);
            controller.DeleteConfirmed(2);
            ActionResult result = controller.Delete(2);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult), "The room exists");
        }

        [TestMethod]
        public void Update_ShouldUpdateRoom()
        {
            mockContext = new Mock<IHotelAC>();
            mockSet = new Mock<DbSet<Soba>>();
            mockContext.Setup(m => m.Sobe).Returns(mockSet.Object);

            var controller = new SobaController(mockContext.Object);
            var newRoom = new Soba
            {
                BrojSobe = 100,
                HotelID = 1,
                SobaID = 1,
                TipSobeID = 1
            };

            controller.Create(newRoom);
            newRoom.BrojSobe = 200;
            controller.Edit(newRoom);

            Assert.IsTrue(200 == newRoom.BrojSobe, "The room did not update");
        }

        [TestMethod]
        public void Index_ShouldReturnSameCountOfRooms()
        {
            var context = new TestHotelContext();

            Hotel p = new Hotel();
            TipSobe c = new TipSobe();
            context.Hoteli.Add(p);
            context.TipSoba.Add(c);

            var controller = new SobaController(context);
            controller.Create(new Soba { Hotel = p, TipSobe = c });
            controller.Create(new Soba { Hotel = p, TipSobe = c });
            controller.Create(new Soba { Hotel = p, TipSobe = c });

            var result = controller.Index() as ViewResult;
            var roomData = (IList<Soba>)result.ViewData.Model;

            Assert.IsNotNull(roomData, "There's no room data");
            Assert.AreEqual(3, roomData.Count, "The count does not match");
        }
    }
}
