using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Hosting;

using CDRTools.DBServices;
using CDRTools.ReportsServices;

namespace CDRTools.Models
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Container()
        {
            return PartialView("_Reports.Container");
        }

        public ActionResult GetReporte(string reporte)
        {
            LlamadasDBService llamadasService = new LlamadasDBService();

            var data = llamadasService.Llamadas_Recupera(1);

            var callDetailsCount = data.Item1;
            var callDetails = data.Item2;

            return View(callDetails);
        }

        public ActionResult GetReport(string reporte, int page)
        {
            int pageToLoad = page;

            if (pageToLoad == 0)
            {
                pageToLoad = 1;
            }

            LlamadasDBService llamadasService = new LlamadasDBService();

            var data = llamadasService.Llamadas_Recupera(pageToLoad);

            var callDetailsCount = data.Item1;
            var callDetails = data.Item2;

            var pager = new Pager(callDetailsCount, pageToLoad);

            var viewModel = new IndexViewModel
            {
                Llamadas = callDetails, // callDetails.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager
            };

            return PartialView("GetReporte",viewModel);
        }

/*
        public ActionResult GetReport_FirstVersion(string reporte)
        {
            int page = 1;

            LlamadasDBService llamadasService = new LlamadasDBService();

            var callDetails = llamadasService.Llamadas_Recupera();

            var pager = new Pager(callDetails.Count(), page);

            var viewModel = new IndexViewModel
            {
                Llamadas = callDetails.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager
            };

            return PartialView("GetReporte", viewModel);
        }
*/

        public FileResult CreateReport()
        {
            DateTime dTime = DateTime.Now;

            MemoryStream workStream = new MemoryStream();

            byte[] result = LlamadasReportService.CreatePDF();

            workStream.Write(result, 0, result.Length);
            workStream.Position = 0;

            var strPDFFileName = string.Format("SamplePDF" + "-" + dTime.ToString("yyyyMMdd") + ".pdf");
            var exportFolder = HostingEnvironment.MapPath("~/Downloads/" + strPDFFileName);

            return File(workStream, "application/pdf", strPDFFileName);
        }
    }
}