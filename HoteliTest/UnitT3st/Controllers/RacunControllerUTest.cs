using HoteliTest.Controllers;
using HoteliTest.Models;
using HoteliTest.ViewModels;
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
   public class RacunControllerUTest:BaseControllerUTest
    {
        private RacunController racunController = new RacunController(testContext.Object);
        private StavkaRacunaController stavkaController = new StavkaRacunaController(testContext.Object);

        [TestMethod]
        public void ConstructorDefaultConstructorVracaNull()
        {
            var racunController = new RacunController();

            Assert.IsNotNull(racunController, "Default konstruktor vraca null");
        }

        [TestMethod]
        public void IndexViewResultVracaNull()
        {
            var rezultat = racunController.Index() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void IndexViewResultVracaTocanTipObjekta()
        {
            var rezultat = racunController.Index() as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(IEnumerable<Racun>), "Krivi tip modela");
        }

        [TestMethod]
        public void CreateViewResultVracaNotNull()
        {
            var rezultat = racunController.Create() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void CreateCreatePostVracaRedirect()
        {
            Racun racun = ModelLoader.GetValidRacun();
            Rezervacija rezervacija = ModelLoader.GetValidRezervacija();

            var rezultat = racunController.Create(racun,rezervacija,2);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Krivi tip modela");
        }

        [TestMethod]
        public void CreateCreatePostNotValidVracaView()
        {
            Racun racun = ModelLoader.GetInvalidRacun();
            Rezervacija rezervacija = ModelLoader.GetInvalidRezervacija();
            racunController.ModelState.AddModelError("RezervacijaID", "ID rezervacije je obavezan");

            var rezultat = racunController.Create(racun, rezervacija, 3) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(Racun), "Krivi model");
        }

        [TestMethod]
        public void DetailsViewResultVracaNotNull()
        {

            var rezultat = racunController.Details(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void EditViewResultVracaNotNull()
        {
            var rezultat = racunController.Edit(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Edit view rezultat je null");
        }

         [TestMethod]
         public void EditEditPostVracaRedirect()
         {
             Racun id = testContext.Object.Racuni.Find(postojeciID);
             var rezultat = racunController.Edit(id);

             Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Valjani edit ne vraca redirect");
         }

        [TestMethod]
        public void EditEditSaNullIDVracaBadRequest()
        {
            int? racunIDnull = null;

            var rezultat = racunController.Edit(racunIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null gost ID ne vraca BadRequest");
        }

        [TestMethod]
        public void EditNepostojeciGostVracaNotFound()
        {
            Racun racun = ModelLoader.GetInvalidRacun();
            HttpStatusCodeResult rezultat = racunController.Edit(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljani gost ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteViewResultVracaNotNull()
        {
            var rezultat = racunController.Delete(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Delete view rezultat je null");
        }

        [TestMethod]
        public void DeleteDeleteSaNullIDVracaBadRequest()
        {
            int? racunIDnull = null;
            var rezultat = racunController.Delete(racunIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null ID ne vraca Bad Request");
        }

        [TestMethod]
        public void DeleteNepostojeciGostVracaNotFound()
        {
            Racun racun = ModelLoader.GetInvalidRacun();
            HttpStatusCodeResult rezultat = racunController.Delete(nepostojeciID) as HttpStatusCodeResult;


            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljani gost ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteConfirmedValjaniDeleteVracaRedirect()
        {
            var rezultat = racunController.DeleteConfirmed(brisaniID);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Delete Confirmed ne vraca redirect");
        }

        [TestMethod]
        public void OdlazakViewResultVracaNotNull()
        {
            var uslugaView = ModelLoader.GetValidUslugaViewModel();
            var rezultat = racunController.Odlazak(1,1,1,uslugaView) as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void Odlazak_ViewResult_ReturnsCorrectObjectType()
        {
            var test = ModelLoader.GetValidUslugaViewModel();
            var result = racunController.Odlazak(1,1,1,test) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(UslugeViewModel), "Wrong Model type");
        }
    }
}
