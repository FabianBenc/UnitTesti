using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoteliTest.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("Error");
        }

        public ActionResult FileNotFound()
        {
            Response.StatusCode = 404;
            return View("FileNotFound");
        }

        public ActionResult InternalServerError()
        {
            Response.StatusCode = 500;
            return View("InternalServerError");
        }
    }
}