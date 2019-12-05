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
    public class GostControllerUTest:BaseControllerUTest
    {
        private GostController gostController = new GostController(testContext.Object);

        [TestMethod]
        public void ConstructorDefaultCOnstructorVracaNull()
        {
            var gostController = new GostController();

            Assert.IsNotNull(gostController, "Default konstruktor vraca null");

        }

        [TestMethod]
        public void IndexViewResultVracaNull()
        {
            var rezultat = gostController.Index("test","test","test",1) as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void IndexViewResultVracaTocanTipObjekta()
        {
            var rezultat = gostController.Index("test", "test", "test", 1) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(IEnumerable<Gost>), "Krivi tip modela");
        }

        [TestMethod]
        public void CreateViewResultVracaNotNull()
        {
            var rezultat = gostController.Create() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void CreateCreatePostVracaRedirect()
        {
            Gost gost = ModelLoader.GetValidGost();
            var rezultat = gostController.Create(gost);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Uspjesno stvaranje gosta ne vraca preusmjeravanje");
        }

        [TestMethod]
        public void CreateCreatePostNotValidVracaView()
        {
            Gost gost = ModelLoader.GetInvalidGost();
            gostController.ModelState.AddModelError("Prezme", "Prezime je obavezno");

            var rezultat = gostController.Create(gost) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(Gost), "Krivi model");
        }

        [TestMethod]
        public void DetailsViewResultVracaNotNull()
        {
            var rezultat = gostController.Details(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat,"View je null");
            Assert.IsInstanceOfType(rezultat.Model, typeof(Gost), "Krivi tip modela");
        }

        [TestMethod]
        public void DetailsNepostojeciIDVracaNotFound()
        {
            var rezultat = gostController.Details(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nepostojeci ID ne vraca NotFound");
        }

        [TestMethod]
        public void DetailsIDjeNullVracaBadRequest()
        {
            var rezultat = gostController.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "null ne vraca Bad Request");
        }

        [TestMethod]
        public void EditViewResultVracaNotNull()
        {
            var rezultat = gostController.Edit(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Edit view rezultat je null");
        }

        [TestMethod]
        public void EditEditPostVracaRedirect()
        {
            Gost gost = testContext.Object.Gosti.Find(postojeciID);
            var rezultat = gostController.Edit(gost);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Valjani edit ne vraca redirect");
        }

        [TestMethod]
        public void EditEditPostNijeValjanVracaView()
        {
            Gost gost = ModelLoader.GetInvalidGost();
            gostController.ModelState.AddModelError("Prezime", "Prezime je obavezno");

            var rezultat = gostController.Edit(gost) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(Gost), "Krvi model");
        }

        [TestMethod]
        public void EditEditSaNullIDVracaBadRequest()
        {
            int? gostIDnull = null;

            var rezultat = gostController.Edit(gostIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null gost ID ne vraca BadRequest");
        }

        [TestMethod]
        public void EditNepostojeciGostVracaNotFound()
        {
            Gost gost = ModelLoader.GetInvalidGost();
            HttpStatusCodeResult rezultat = gostController.Edit(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljani gost ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteViewResultVracaNotNull()
        {
            var rezultat = gostController.Delete(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Delete view rezultat je null");
        }

        [TestMethod]
        public void DeleteDeleteSaNullIDVracaBadRequest()
        {
            int? gostIDnull=null;
            var rezultat = gostController.Delete(gostIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null ID ne vraca Bad Request");
        }

        [TestMethod]
        public void DeleteNepostojeciGostVracaNotFound()
        {
            Gost gost = ModelLoader.GetInvalidGost();
            HttpStatusCodeResult rezultat = gostController.Delete(nepostojeciID) as HttpStatusCodeResult;


            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljani gost ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteConfirmedValjaniDeleteVracaRedirect()
        {
            var rezultat = gostController.DeleteConfirmed(brisaniID);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Delete Confirmed ne vraca redirect");
        }
    }
}
