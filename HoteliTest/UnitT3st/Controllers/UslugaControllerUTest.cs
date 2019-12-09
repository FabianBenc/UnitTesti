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
    public class UslugaControllerUTest : BaseControllerUTest
    {
        private UslugaController uslugaController = new UslugaController(testContext.Object);

        [TestMethod]
        public void ConstructorDefaultConstructorVracaNotNull()
        {
            var uslugaController = new UslugaController();

            Assert.IsNotNull(uslugaController, "Default konstruktor vraca null");

        }

        [TestMethod]
        public void IndexViewResultVracaNull()
        {
            var rezultat = uslugaController.Index() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
            Assert.IsInstanceOfType(rezultat.Model, typeof(IEnumerable<Usluga>), "Krivi tip modela");

        }

        [TestMethod]
        public void CreateViewResultVracaNotNull()
        {
            var rezultat = uslugaController.Create() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void CreateCreatePostVracaRedirect()
        {
            Usluga usluga = ModelLoader.GetValidUsluga();
            var rezultat = uslugaController.Create(usluga);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Uspjesno stvaranje usluge ne vraca preusmjeravanje");
        }

        [TestMethod]
        public void CreateCreatePostNotValidVracaView()
        {
            Usluga usluga = ModelLoader.GetInvalidUsluga();
            uslugaController.ModelState.AddModelError("ImeUsluge", "Ime usluge je obavezno");

            var rezultat = uslugaController.Create(usluga) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(Usluga), "Krivi model");
        }

        [TestMethod]
        public void DetailsViewResultVracaNotNull()
        {
            var rezultat = uslugaController.Details(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
            Assert.IsInstanceOfType(rezultat.Model, typeof(Usluga), "Krivi tip modela");
        }

        [TestMethod]
        public void DetailsNepostojeciIDVracaNotFound()
        {
            var rezultat = uslugaController.Details(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nepostojeci ID ne vraca NotFound");
        }

        [TestMethod]
        public void DetailsIDjeNullVracaBadRequest()
        {
            var rezultat = uslugaController.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "null ne vraca Bad Request");
        }

        [TestMethod]
        public void EditViewResultVracaNotNull()
        {
            var rezultat = uslugaController.Edit(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Edit view rezultat je null");
        }

        [TestMethod]
        public void EditEditPostVracaRedirect()
        {
            Usluga usluga = testContext.Object.Usluge.Find(postojeciID);
            var rezultat = uslugaController.Edit(usluga);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Valjani edit ne vraca redirect");
        }

        [TestMethod]
        public void EditEditPostNijeValjanVracaView()
        {
            Usluga usluga = ModelLoader.GetInvalidUsluga();
            uslugaController.ModelState.AddModelError("ImeUsluge", "Ime usluge je obavezno");

            var rezultat = uslugaController.Edit(usluga) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(Usluga), "Krvi model");
        }

        [TestMethod]
        public void EditEditSaNullIDVracaBadRequest()
        {
            int? uslugaIDnull = null;

            var rezultat = uslugaController.Edit(uslugaIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null usluga ID ne vraca BadRequest");
        }

        [TestMethod]
        public void EditNepostojecaUslugaVracaNotFound()
        {
            Usluga usluga = ModelLoader.GetInvalidUsluga();
            HttpStatusCodeResult rezultat = uslugaController.Edit(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljana usluga ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteViewResultVracaNotNull()
        {
            var rezultat = uslugaController.Delete(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Delete view rezultat je null");
        }

        [TestMethod]
        public void DeleteDeleteSaNullIDVracaBadRequest()
        {
            int? uslugaIDnull = null;
            var rezultat = uslugaController.Delete(uslugaIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null ID ne vraca Bad Request");
        }

        [TestMethod]
        public void DeleteNepostojecaUslugaVracaNotFound()
        {
            Usluga usluga = ModelLoader.GetInvalidUsluga();
            HttpStatusCodeResult rezultat = uslugaController.Delete(nepostojeciID) as HttpStatusCodeResult;


            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljana usluga ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteConfirmedValjaniDeleteVracaRedirect()
        {
            var rezultat = uslugaController.DeleteConfirmed(brisaniID);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Delete Confirmed ne vraca redirect");
        }
    }
}
