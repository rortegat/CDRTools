using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CDRTools.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            bool sessionInit = Session["sessionInit"] == null ? false : (bool)Session["sessionInit"];

            if (!sessionInit)
            {
                return RedirectToAction("Index", "Llamada");
            }

            ViewBag.Title = "Hola";

            return View();
        }
    }
}