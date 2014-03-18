using System;
using System.Web.Mvc;
 
namespace Bingo.Mobile.Controllers
{
    public class FrontEndViewsController : Controller
    {
        public ActionResult Shell()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }
 
        public ActionResult Companies()
        { 
            return View();
        }

        public ActionResult Company()
        {
            ViewBag.SomeVariable = "blablabla";

            return View();
        }
    }
}

