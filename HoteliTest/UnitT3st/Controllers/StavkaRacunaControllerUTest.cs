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
    public class StavkaControllerUTest : BaseControllerUTest
    {
        private StavkaRacunaController stavkaController = new StavkaRacunaController(testContext.Object);

        [TestMethod]
        public void ConstructorDefaultConstructorVracaNotNull()
        {
            var stavkaController = new StavkaRacunaController();

            Assert.IsNotNull(stavkaController, "Default konstruktor vraca null");

        }

        [TestMethod]
        public void IndexViewResultVracaNull()
        {
            var rezultat = stavkaController.Index() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
            Assert.IsInstanceOfType(rezultat.Model, typeof(IEnumerable<StavkaRacuna>), "Krivi tip modela");

        }

        [TestMethod]
        public void CreateViewResultVracaNotNull()
        {
            var controller = new StavkaRacunaController();
            var rezultat = controller.Create() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void CreateCreatePostVracaRedirect()
        {
            Usluga usluga = ModelLoader.GetValidUsluga();   
            StavkaRacuna stavka = ModelLoader.GetValidStavka();
            var rezultat = stavkaController.Create(stavka,1,usluga);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Uspjesno stvaranje stavke ne vraca preusmjeravanje");
        }

        [TestMethod]
        public void CreateCreatePostNotValidVracaView()
        {
            Usluga usluga = ModelLoader.GetValidUsluga();
            StavkaRacuna stavka = ModelLoader.GetInvalidStavka();
            stavkaController.ModelState.AddModelError("Kolicina", "Kolicina je obavezna");

            var rezultat = stavkaController.Create(stavka,2,usluga) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(StavkaRacuna), "Krivi model");
        }

        [TestMethod]
        public void DetailsViewResultVracaNotNull()
        {
            var rezultat = stavkaController.Details(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
            Assert.IsInstanceOfType(rezultat.Model, typeof(StavkaRacuna), "Krivi tip modela");
        }

        [TestMethod]
        public void DetailsNepostojeciIDVracaNotFound()
        {
            var rezultat = stavkaController.Details(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nepostojeci ID ne vraca NotFound");
        }

        [TestMethod]
        public void DetailsIDjeNullVracaBadRequest()
        {
            var rezultat = stavkaController.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "null ne vraca Bad Request");
        }

        [TestMethod]
        public void EditViewResultVracaNotNull()
        {
            var rezultat = stavkaController.Edit(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Edit view rezultat je null");
        }

        [TestMethod]
        public void EditEditPostVracaRedirect()
        {
            StavkaRacuna stavka = testContext.Object.StavkeRacuna.Find(postojeciID);
            var rezultat = stavkaController.Edit(stavka);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Valjani edit ne vraca redirect");
        }

        [TestMethod]
        public void EditEditPostNijeValjanVracaView()
        {
            StavkaRacuna stavka = ModelLoader.GetInvalidStavka();
            stavkaController.ModelState.AddModelError("BrojSobe", "Broj sobe je obavezan");

            var rezultat = stavkaController.Edit(stavka) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(StavkaRacuna), "Krvi model");
        }

        [TestMethod]
        public void EditEditSaNullIDVracaBadRequest()
        {
            int? stavkaIDnull = null;

            var rezultat = stavkaController.Edit(stavkaIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null stavka racuna ID ne vraca BadRequest");
        }

        [TestMethod]
        public void EditNepostojecaSobaVracaNotFound()
        {
            StavkaRacuna stavka = ModelLoader.GetInvalidStavka();
            HttpStatusCodeResult rezultat = stavkaController.Edit(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljani gost ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteViewResultVracaNotNull()
        {
            var rezultat = stavkaController.Delete(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Delete view rezultat je null");
        }

        [TestMethod]
        public void DeleteDeleteSaNullIDVracaBadRequest()
        {
            int? stavkaIDnull = null;
            var rezultat = stavkaController.Delete(stavkaIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null ID ne vraca Bad Request");
        }

        [TestMethod]
        public void DeleteNepostojecaSobaVracaNotFound()
        {
            StavkaRacuna stavka = ModelLoader.GetInvalidStavka();
            HttpStatusCodeResult rezultat = stavkaController.Delete(nepostojeciID) as HttpStatusCodeResult;


            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljani gost ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteConfirmedValjaniDeleteVracaRedirect()
        {
            var rezultat = stavkaController.DeleteConfirmed(brisaniID);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Delete Confirmed ne vraca redirect");
        }
    }
}
