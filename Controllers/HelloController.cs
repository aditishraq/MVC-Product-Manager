using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class HelloController : Controller
    {
        // GET: Hello
        public ActionResult Index()
        {
            // Passing a simple message to the view using ViewBag
            ViewBag.Message = "Hello, World!";
            return View();
        }
    }
}