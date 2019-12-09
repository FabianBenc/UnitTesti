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
    public class TipSobeControllerUTest : BaseControllerUTest
    {
        private TipSobeController tipSobeController = new TipSobeController(testContext.Object);

        [TestMethod]
        public void ConstructorDefaultConstructorVracaNotNull()
        {
            var tipSobeController = new TipSobeController();

            Assert.IsNotNull(tipSobeController, "Default konstruktor vraca null");

        }

        [TestMethod]
        public void IndexViewResultVracaNull()
        {
            var rezultat = tipSobeController.Index() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
            Assert.IsInstanceOfType(rezultat.Model, typeof(IEnumerable<TipSobe>), "Krivi tip modela");

        }

        [TestMethod]
        public void CreateViewResultVracaNotNull()
        {
            var rezultat = tipSobeController.Create() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void CreateCreatePostVracaRedirect()
        {
            TipSobe tipSobe = ModelLoader.GetValidTipSobe();
            var rezultat = tipSobeController.Create(tipSobe);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Uspjesno stvaranje tipa sobe ne vraca preusmjeravanje");
        }

        [TestMethod]
        public void CreateCreatePostNotValidVracaView()
        {
            TipSobe tipSobe = ModelLoader.GetInvalidTipSobe();
            tipSobeController.ModelState.AddModelError("CijenaPoNoci", "Cijena je obavezna");

            var rezultat = tipSobeController.Create(tipSobe) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(TipSobe), "Krivi model");
        }

        [TestMethod]
        public void DetailsViewResultVracaNotNull()
        {
            var rezultat = tipSobeController.Details(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
            Assert.IsInstanceOfType(rezultat.Model, typeof(TipSobe), "Krivi tip modela");
        }

        [TestMethod]
        public void DetailsNepostojeciIDVracaNotFound()
        {
            var rezultat = tipSobeController.Details(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nepostojeci ID ne vraca NotFound");
        }

        [TestMethod]
        public void DetailsIDjeNullVracaBadRequest()
        {
            var rezultat = tipSobeController.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "null ne vraca Bad Request");
        }

        [TestMethod]
        public void EditViewResultVracaNotNull()
        {
            var rezultat = tipSobeController.Edit(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Edit view rezultat je null");
        }

        [TestMethod]
        public void EditEditPostVracaRedirect()
        {
            TipSobe tipSobe = testContext.Object.TipSoba.Find(postojeciID);
            var rezultat = tipSobeController.Edit(tipSobe);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Valjani edit ne vraca redirect");
        }

        [TestMethod]
        public void EditEditPostNijeValjanVracaView()
        {
            TipSobe tipSobe = ModelLoader.GetInvalidTipSobe();
            tipSobeController.ModelState.AddModelError("CijenaPoNoci", "Cijena je obavezna");

            var rezultat = tipSobeController.Edit(tipSobe) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(TipSobe), "Krvi model");
        }

        [TestMethod]
        public void EditEditSaNullIDVracaBadRequest()
        {
            int? tipSobeIDnull = null;

            var rezultat = tipSobeController.Edit(tipSobeIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null gost ID ne vraca BadRequest");
        }

        [TestMethod]
        public void EditNepostojeciTipSobeVracaNotFound()
        {
            TipSobe tipSobe = ModelLoader.GetInvalidTipSobe();
            HttpStatusCodeResult rezultat = tipSobeController.Edit(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljani tip sobe ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteViewResultVracaNotNull()
        {
            var rezultat = tipSobeController.Delete(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Delete view rezultat je null");
        }

        [TestMethod]
        public void DeleteDeleteSaNullIDVracaBadRequest()
        {
            int? tipSobeIDnull = null;
            var rezultat = tipSobeController.Delete(tipSobeIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null ID ne vraca Bad Request");
        }

        [TestMethod]
        public void DeleteNepostojeciTipSobeVracaNotFound()
        {
            TipSobe tipSobe = ModelLoader.GetInvalidTipSobe();
            HttpStatusCodeResult rezultat = tipSobeController.Delete(nepostojeciID) as HttpStatusCodeResult;


            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljani tip sobe ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteConfirmedValjaniDeleteVracaRedirect()
        {
            var rezultat = tipSobeController.DeleteConfirmed(brisaniID);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Delete Confirmed ne vraca redirect");
        }
    }
}
