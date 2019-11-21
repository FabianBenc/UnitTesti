using HoteliTest.DAL;
using HoteliTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoteliTest.Controllers
{
    public class BaseController : Controller
    {
        public IHotelAC db = new HotelContext();
        protected readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            logger.Error(filterContext.Exception.Message);
            filterContext.Result = View("Error", new HandleErrorInfo(filterContext.Exception, filterContext.RouteData.Values["controller"].ToString(),
                filterContext.RouteData.Values["action"].ToString()));
        }
    }
}