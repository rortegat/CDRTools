using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CDRTools.DBServices;
using CDRTools.Helpers;
using System.Net;
using System.Net.Mime;

namespace CDRTools.Controllers
{
    public class LlamadaController : Controller
    {
        [HttpPost]
        public ActionResult CargaLlamadas()
        {
            LlamadasDBService dbServiceLlamadas = new LlamadasDBService();

            string messageError = dbServiceLlamadas.Llamadas_Carga();
            bool statusProcess = false;

            Session["messageError"] = messageError;
            Session["sessionInit"] = true;

            if (!string.IsNullOrEmpty(messageError))
            {
                //Response.StatusCode = (int)HttpStatusCode.BadRequest;
                statusProcess = false;
            }
            else
            {
                //Response.StatusCode = (int)HttpStatusCode.OK;
                statusProcess = true;
            }

            return Json(new { success = statusProcess, message = messageError }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNotification(string message)
        {
            ViewBag.message = message;

            return PartialView("_Notifications.ModalPreview");
        }

        // GET: Llamada
        public ActionResult Index()
        {
            if (Session["sessionInit"] == null)
            {
                Session["sessionInit"] = false;
            }

            return View();
        }

        // GET: Llamadda/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Llamadda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Llamadda/Create
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

        // GET: Llamadda/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Llamadda/Edit/5
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

        // GET: Llamadda/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Llamadda/Delete/5
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
