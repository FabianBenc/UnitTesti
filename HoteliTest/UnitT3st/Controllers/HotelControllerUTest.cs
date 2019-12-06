using HoteliTest.Controllers;
using HoteliTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UnitT3st.Controllers
{
    [TestClass]
   public class HotelControllerUTest : BaseControllerUTest
    {
        private HotelController hotelController = new HotelController(testContext.Object);

        [TestMethod]
        public void ConstructorDefaultConstructorVracaNotNull()
        {
            var hotelController = new HotelController();

            Assert.IsNotNull(hotelController,"Default constructor vraca null vrijednost");
        }

        [TestMethod]
        public void IndexViewResultVracaNotNull()
        {
            var rezultat = hotelController.Index() as ViewResult;

            Assert.IsNotNull(rezultat, "View nije null");
            Assert.IsInstanceOfType(rezultat.Model, typeof(IEnumerable<Hotel>), "Krivi tip modela");
        }

        [TestMethod]
        public void CreateViewResultVracaNotNull()
        {
            var rezultat = hotelController.Create() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void CreateCreatePostReturnsRedirect()
        {
            Hotel hotel = ModelLoader.GetValidHotel();

            var rezultat = hotelController.Create(hotel);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Uspjesan create nije vratio redirect");
        }

        [TestMethod]
        public void CreateCreatePostNotValidVracaView()
        {
            Hotel hotel = ModelLoader.GetInvalidHotel();
            hotelController.ModelState.AddModelError("Ime", "Ime hotela je obavezno");

            var rezultat = hotelController.Create(hotel) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(Hotel), "Krivi model");
        }

        [TestMethod]
        public void DetailsViewResultVracaNotNull()
        {
            var rezultat = hotelController.Details(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
            Assert.IsInstanceOfType(rezultat.Model, typeof(Hotel), "Krivi tip modela");
        }

        [TestMethod]
        public void DetailsNepostojeciIDVracaNotFound()
        {
            var rezultat = hotelController.Details(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nepostojeci ID ne vraca NotFound");
        }

        [TestMethod]
        public void DetailsIDjeNullVracaBadRequest()
        {
            var rezultat = hotelController.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null ne vraca Bad Request");
        }

        [TestMethod]
        public void EditViewResultVracaNotNull()
        {
            var rezultat = hotelController.Edit(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Edit view result je null");
        }

        [TestMethod]
        public void EditEditPostVracaRedirect()
        {
            Hotel hotel = testContext.Object.Hoteli.Find(postojeciID);
            var rezultat = hotelController.Edit(hotel);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Valjani edit ne vraca redirect ");
        }

        [TestMethod]
        public void EditEditPostNotValidVracaView()
        {
            Hotel hotel = ModelLoader.GetInvalidHotel();
            hotelController.ModelState.AddModelError("Ime", "Ime je obavezno");

            var rezultat = hotelController.Edit(hotel) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(Hotel), "Krivi model");
        }

        [TestMethod]
        public void EdiEditSaNullIDVracaBadRequest()
        {
            int? hotelNullID = null;
            var rezultat = hotelController.Edit(hotelNullID) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null ID ne vraca Bad Request");
        }

        [TestMethod]
        public void EditNepostojeciHotelVracaNotFound()
        {
            Hotel hotel = ModelLoader.GetInvalidHotel();
            var rezultat = hotelController.Edit(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Invalid hotel ne vraca Not Found ");
        }

        [TestMethod]
        public void DeleteViewResultVracaNotNull()
        {
            var rezultat = hotelController.Delete(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Delete view result je null");
        }

        [TestMethod]
        public void DeleteDeleteSaNullIDVracaBadREquest()
        {
            int? hotelNullID = null;
            var rezultat = hotelController.Delete(hotelNullID) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null ID ne vraca Bad Request");
        }

        [TestMethod]
        public void DeleteNepostojeciHotelVracaNotFound()
        {
            Hotel hotel = ModelLoader.GetInvalidHotel();
            var rezultat = hotelController.Delete(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Invalid hotel ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteConfirmedValidDeleteVracaRedirect()
        {
            var rezultat = hotelController.DeleteConfirmed(brisaniID);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Delete confirmed ne vraca redirect");
        }
    }
}
