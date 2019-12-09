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
    }
}
