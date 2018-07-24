using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Util;

using CDRTools.DBServices;
using CDRTools.Models;

namespace CDRTools.ReportsServices
{
    public static class LlamadasReportService
    {
        public static void CreaReporte()
        {
            var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var exportFile = System.IO.Path.Combine(exportFolder, "Test.pdf");

            using (var writer = new PdfWriter(exportFile))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var doc = new Document(pdf);
                    doc.Add(new Paragraph("Hello World!"));
                }
            }
        }

        public static byte[] CreatePDF()
        {
            // Data Services
            LlamadasDBService llamadasService = new LlamadasDBService();

            var data = llamadasService.Llamadas_Recupera(1);
            var callDetails = data.Item2;

            // Memory stream
            MemoryStream workStream = new MemoryStream();

            //file name to be created
            //1
            //var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //var strPDFFileName = System.IO.Path.Combine(exportFolder, "SamplePDF.pdf");

            //2

            var writer = new PdfWriter(workStream);
            var pdf = new PdfDocument(writer);

            var document = new Document(pdf, iText.Kernel.Geom.PageSize.LETTER);
            document.SetMargins(20, 20, 20, 20);

            var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            var bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            Table table = new Table(new float[] { 10, 1, 3, 4 });
            table.SetWidth(iText.Layout.Properties.UnitValue.CreatePercentValue(100));

            //process(table, line, bold, true);

            foreach (var llamada in callDetails)
            {
                process(table, llamada, font, false);
            }

            document.Add(table);
            document.Close();

            byte[] result = workStream.ToArray();

            return result;
        }

        public static void CreaterPDF()
        {
            LlamadasDBService llamadasService = new LlamadasDBService();

            var data = llamadasService.Llamadas_Recupera(1);
            var callDetails = data.Item2;

            //file name to be created
            var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var strPDFFileName = System.IO.Path.Combine(exportFolder, "SamplePDF.pdf");

            using (var writer = new PdfWriter(strPDFFileName))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf,iText.Kernel.Geom.PageSize.LETTER);
                    document.SetMargins(20, 20, 20, 20);
                    var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    var bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    Table table = new Table(new float[] { 10, 1, 3, 4 });
                    table.SetWidth(iText.Layout.Properties.UnitValue.CreatePercentValue(100));

                    //process(table, line, bold, true);

                    foreach (var llamada in callDetails)
                    {
                        process(table, llamada, font, false);
                    }

                    document.Add(table);
                    document.Close();
                }
            }

        }

        private static void process(Table table, Llamada detail, PdfFont font, bool isHeader)
        {
                if (isHeader)
                {
                    table.AddHeaderCell(
                        new Cell().Add(
                            new Paragraph().SetFont(font)));
                }
                else
                {
                    table.AddCell(
                        new Cell().Add(
                            new Paragraph(detail.dateTimeOrigination.ToString()).SetFont(font)));
                    table.AddCell(
                        new Cell().Add(
                            new Paragraph(detail.callingPartyNumber).SetFont(font)));
                    table.AddCell(
                        new Cell().Add(
                            new Paragraph(detail.originalCalledPartyNumber).SetFont(font)));
                    table.AddCell(
                        new Cell().Add(
                            new Paragraph(detail.duration.ToString()).SetFont(font)));
                }
        }
    }
}