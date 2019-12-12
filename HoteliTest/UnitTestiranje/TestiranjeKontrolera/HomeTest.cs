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
    public class HomeTest
    {
        [TestMethod]
        public void Index()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result, "The view is empty");
        }

        [TestMethod]
        public void About()
        {
            var controller = new HomeController();
            var result = controller.About() as ViewResult;
            Assert.IsNotNull(result, "The view is empty");
        }

        [TestMethod]
        public void Contact()
        {
            var controller = new HomeController();
            var result = controller.Contact() as ViewResult;
            Assert.IsNotNull(result, "The view is empty");
        }
    }
}
