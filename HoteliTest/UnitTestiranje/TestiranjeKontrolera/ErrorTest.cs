using HoteliTest.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UnitTestiranje.TestiranjeKontrolera
{
    [TestClass]
    public class ErrorTest
    {
        private readonly ErrorController errorController = new ErrorController();

        [TestMethod]
        public void Index_ReturnsView_ReturnsTrue()
        {
            var result = errorController.Index() as ViewResult;

            Assert.IsNotNull(result, "View is null");
        }

        [TestMethod]
        public void FileNotFound_ReturnsView_ReturnsTrue()
        {
            var result = errorController.FileNotFound() as ViewResult;

            Assert.IsNotNull(result, "View is null");
        }

        [TestMethod]
        public void InternalServerError_ReturnsView_ReturnsTrue()
        {
            var result = errorController.InternalServerError() as ViewResult;

            Assert.IsNotNull(result, "View is null");
        }
    }
}
