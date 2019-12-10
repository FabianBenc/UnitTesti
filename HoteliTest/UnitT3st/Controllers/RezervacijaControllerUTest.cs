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
    public class RezervacijaControllerUTest : BaseControllerUTest
    {
        private RezervacijaController rezervacijaController = new RezervacijaController(testContext.Object);

        RezervacijaView rezervacijaView = new RezervacijaView
        {
            RezervacijaID = 2,
            GostID = 1,
            SobaID = 1,
            Popust = 99,
            Prijava = new DateTime(2019, 4, 1),
            Odjava = new DateTime(2019, 5, 1)
        };

        [TestMethod]
        public void ConstructorDefaultConstructorVracaNotNull()
        {
            var rezervacijaController = new RezervacijaController();

            Assert.IsNotNull(rezervacijaController, "Default constructor vraca null vrijednost");
        }

        [TestMethod]
        public void IndexViewResultVracaNotNull()
        {
            var rezultat = rezervacijaController.Index() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void IndexViewResultVracaCorrectObjectType()
        {
            var rezultat = rezervacijaController.Index() as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(IEnumerable<Rezervacija>), "Krivi tip modela");
        }

        [TestMethod]
        public void CreateActionResultVracaNotNUll()
        {
            var rezervacijaView = ModelLoader.GetValidRezervacijaView();
            var rezultat = rezervacijaController.Create(rezervacijaView) as ActionResult;

            Assert.IsNotNull(rezultat, "View ima null vrijednost");
        }

        [TestMethod]
        public void DetailsViewResultVracaNotNull()
        {
            var rezultat = rezervacijaController.Details(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
            Assert.IsInstanceOfType(rezultat.Model, typeof(Rezervacija), "Postojeci detalji rezervacije ne vracaju redirect");
        }

        [TestMethod]
        public void DetailsNepostojeciIDVracaNotFound()
        {
            var rezultat = rezervacijaController.Details(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nepostojeci ID new vraca NotFound");
        }

        [TestMethod]
        public void DetailsIDjeNullVracaBadRequest()
        {
            var rezultat = rezervacijaController.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null ne vraca Bad Request");
        }

        [TestMethod]
        public void EditViewResultVracaNotNull()
        {
            var rezultat = rezervacijaController.Edit(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Edit view rezultat je null");
        }

        [TestMethod]
        public void EditEditPostVracaRedirect()
        {
            Rezervacija id = testContext.Object.Rezervacije.Find(postojeciID);
            var rezultat = rezervacijaController.Edit(id);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Valjani edit ne vraca redirect");
        }

        [TestMethod]
        public void EditEditSaNullIDVracaBadRequest()
        {
            int? rezervacijaIDnull = null;

            var rezultat = rezervacijaController.Edit(rezervacijaIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null gost ID ne vraca BadRequest");
        }

        [TestMethod]
        public void EditNepostojeciGostVracaNotFound()
        {
            Rezervacija rezervacija = ModelLoader.GetInvalidRezervacija();
            HttpStatusCodeResult rezultat = rezervacijaController.Edit(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljani gost ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteViewResultVracaNotNull()
        {
            var rezultat = rezervacijaController.Delete(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Delete view rezultat je null");
        }

        [TestMethod]
        public void DeleteDeleteSaNullIDVracaBadRequest()
        {
            int? rezervacijaIDnull = null;
            var rezultat = rezervacijaController.Delete(rezervacijaIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null ID ne vraca Bad Request");
        }

        [TestMethod]
        public void DeleteNepostojecaRezervacijaVracaNotFound()
        {
            Rezervacija rezervacija = ModelLoader.GetInvalidRezervacija();
            HttpStatusCodeResult rezultat = rezervacijaController.Delete(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljana rezervacija ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteConfirmedValjaniDeleteVracaRedirect()
        {
            var rezultat = rezervacijaController.DeleteConfirmed(brisaniID);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Delete Confirmed ne vraca redirect");
        }

        [TestMethod]
        public void OdabirDatumaViewResultVracaNotNull()
        {
            var rezultat = rezervacijaController.OdabirDatuma() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
           // Assert.IsInstanceOfType(rezultat.Model, typeof(RezervacijaView), "Krivi model");
        }

        [TestMethod]
        public void OdabirDatumaOdabirDatumaPostVracaRedirect()
        {
            var odabirDatuma = ModelLoader.GetValidRezervacijaView();
            var rezultat = rezervacijaController.OdabirDatuma(odabirDatuma);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Uspjesan odabir datuma ne vraca redirect");
        }

        [TestMethod]
        public void OdabirSobePostVracaRedirect()
        {
            var odabirSobe = ModelLoader.GetValidRezervacijaView();
            var rezultat = rezervacijaController.OdabirSobe(odabirSobe);

            Assert.IsInstanceOfType(rezultat, typeof(ViewResult), "Uspjesan odabir datuma ne vraca redirect");
        }
    }
}
