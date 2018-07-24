using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CDRTools.Controllers
{
    public class AutorizacionController : Controller
    {
        // GET: Autorizacion
        public ActionResult Index()
        {
            return View();
        }

        // GET: Autorizacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Autorizacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autorizacion/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Autorizacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Autorizacion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Autorizacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Autorizacion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
