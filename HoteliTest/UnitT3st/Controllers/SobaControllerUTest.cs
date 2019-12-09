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
    public class SobaControllerUTest : BaseControllerUTest
    {
        private SobaController sobaController = new SobaController(testContext.Object);

        [TestMethod]
        public void ConstructorDefaultConstructorVracaNotNull()
        {
            var sobaController = new SobaController();

            Assert.IsNotNull(sobaController, "Default konstruktor vraca null");

        }

        [TestMethod]
        public void IndexViewResultVracaNull()
        {
            var rezultat = sobaController.Index() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
            Assert.IsInstanceOfType(rezultat.Model, typeof(IEnumerable<Soba>), "Krivi tip modela");

        }

        [TestMethod]
        public void CreateViewResultVracaNotNull()
        {
            var controller = new SobaController();
            var rezultat = controller.Create() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void CreateCreatePostVracaRedirect()
        {
            Soba soba = ModelLoader.GetValidSoba();
            var rezultat = sobaController.Create(soba);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Uspjesno stvaranje sobe ne vraca preusmjeravanje");
        }

        [TestMethod]
        public void CreateCreatePostNotValidVracaView()
        {
            Soba soba = ModelLoader.GetInvalidSoba();
            sobaController.ModelState.AddModelError("BrojSobe", "Broj sobe je obavezan");

            var rezultat = sobaController.Create(soba) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(Soba), "Krivi model");
        }

        [TestMethod]
        public void DetailsViewResultVracaNotNull()
        {
            var rezultat = sobaController.Details(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
            Assert.IsInstanceOfType(rezultat.Model, typeof(Soba), "Krivi tip modela");
        }

        [TestMethod]
        public void DetailsNepostojeciIDVracaNotFound()
        {
            var rezultat = sobaController.Details(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nepostojeci ID ne vraca NotFound");
        }

        [TestMethod]
        public void DetailsIDjeNullVracaBadRequest()
        {
            var rezultat = sobaController.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "null ne vraca Bad Request");
        }

        [TestMethod]
        public void EditViewResultVracaNotNull()
        {
            var rezultat = sobaController.Edit(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Edit view rezultat je null");
        }

        [TestMethod]
        public void EditEditPostVracaRedirect()
        {
            Soba soba = testContext.Object.Sobe.Find(postojeciID);
            var rezultat = sobaController.Edit(soba);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Valjani edit ne vraca redirect");
        }

        [TestMethod]
        public void EditEditPostNijeValjanVracaView()
        {
            Soba soba = ModelLoader.GetInvalidSoba();
            sobaController.ModelState.AddModelError("BrojSobe", "Broj sobe je obavezan");

            var rezultat = sobaController.Edit(soba) as ViewResult;

            Assert.IsInstanceOfType(rezultat.Model, typeof(Soba), "Krvi model");
        }

        [TestMethod]
        public void EditEditSaNullIDVracaBadRequest()
        {
            int? sobaIDnull = null;

            var rezultat = sobaController.Edit(sobaIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null soba ID ne vraca BadRequest");
        }

        [TestMethod]
        public void EditNepostojecaSobaVracaNotFound()
        {
            Soba soba = ModelLoader.GetInvalidSoba();
            HttpStatusCodeResult rezultat = sobaController.Edit(nepostojeciID) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljana soba ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteViewResultVracaNotNull()
        {
            var rezultat = sobaController.Delete(postojeciID) as ViewResult;

            Assert.IsNotNull(rezultat, "Delete view rezultat je null");
        }

        [TestMethod]
        public void DeleteDeleteSaNullIDVracaBadRequest()
        {
            int? sobaIDnull = null;
            var rezultat = sobaController.Delete(sobaIDnull) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, rezultat.StatusCode, "Null ID ne vraca Bad Request");
        }

        [TestMethod]
        public void DeleteNepostojecaSobaVracaNotFound()
        {
            Soba soba = ModelLoader.GetInvalidSoba();
            HttpStatusCodeResult rezultat = sobaController.Delete(nepostojeciID) as HttpStatusCodeResult;


            Assert.IsInstanceOfType(rezultat, typeof(HttpNotFoundResult), "Nevaljana soba ne vraca Not Found");
        }

        [TestMethod]
        public void DeleteConfirmedValjaniDeleteVracaRedirect()
        {
            var rezultat = sobaController.DeleteConfirmed(brisaniID);

            Assert.IsInstanceOfType(rezultat, typeof(RedirectToRouteResult), "Delete Confirmed ne vraca redirect");
        }
    }
}
