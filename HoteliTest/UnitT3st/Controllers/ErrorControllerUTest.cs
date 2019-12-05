using HoteliTest.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UnitT3st.Controllers
{
    [TestClass]
    public class ErrorControllerUTest
    {
        private ErrorController errorController = new ErrorController();
        [TestMethod]
        public void IndexVracaView()
        {
            var rezultat = errorController.Index() as ViewResult;

            Assert.IsNotNull(rezultat, "View je null");
        }

        [TestMethod]
        public void FileNotFoundVracaView()
        {
            var rezultat = errorController.FileNotFound() as ViewResult;

            Assert.IsNotNull(rezultat, "View nije null");
        }

        [TestMethod]
        public void InternalServerErrorVracaView()
        {
            var rezultat = errorController.InternalServerError() as ViewResult;

            Assert.IsNotNull(rezultat, "View nije null");
        }
    }
}
