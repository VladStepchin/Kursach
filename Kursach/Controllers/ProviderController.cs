using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kursach.Helpers;
using System.IO;
using HiQPdf;

namespace Kursach.Controllers
{
    public class ProviderController : Controller, IDbHelper<Provider>
    {

        MyDb db = new MyDb();

        public Provider getItemById(int? id)
        {
            var listofSelectedProviders = db.Providers.ToList().Where(x => x.Id == id);

            return listofSelectedProviders.FirstOrDefault();
        }

        public string RenderViewAsString(string viewName, object model)
        {
            // create a string writer to receive the HTML code
            StringWriter stringWriter = new StringWriter();

            // get the view to render
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext,
                      viewName, null);
            // create a context to render a view based on a model
            ViewContext viewContext = new ViewContext(
                ControllerContext,
                viewResult.View,
                new ViewDataDictionary(model),
                new TempDataDictionary(),
                stringWriter
            );

            // render the view to a HTML code
            viewResult.View.Render(viewContext, stringWriter);

            // return the HTML code
            return stringWriter.ToString();
        }

        [HttpGet]
        public ActionResult ConvertHtmlPageToPdfProvider()
        {
            List<VIPProvider> VIPProviders = db.VIPProviders.ToList();

            // get the HTML code of this view
            string htmlToConvert = RenderViewAsString("ListOfProvidersToPDF", VIPProviders);

            // the base URL to resolve relative images and css
            String thisPageUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
            String baseUrl = thisPageUrl.Substring(0, thisPageUrl.Length -
                "Provider/ConvertHtmlPageToPdfProvider".Length);

            // instantiate the HiQPdf HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            //hide the button in the created PDF
            //htmlToPdfConverter.HiddenHtmlElements = new string[] 
            //           { "#convertThisPageButtonDiv" };

            // render the HTML code as PDF in memory
            byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);

            // send the PDF file to browser
            FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");
            fileResult.FileDownloadName = "ThisMvcViewToPdf.pdf";

            return File(pdfBuffer, "application/pdf", "ListOfVIPProviders.pdf");
        } 

        [HttpGet]
        public ActionResult Index()
        {
            List<Provider> Providers = db.Providers.ToList();

            return View(Providers);
        }

        public ActionResult VIPProdivers()
        {
            List<VIPProvider> VIPProviders = db.VIPProviders.ToList();

            return View(VIPProviders);
        }

        [HttpPost]
        public ActionResult Create(Provider provider)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            db.Providers.Add(provider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var provider = getItemById(id);

            if (provider == null)
            {
                return HttpNotFound();
            }

            return PartialView("Delete", provider);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeletConfirm(int? id)
        {

            var provider = getItemById(id);

            db.Providers.Remove(provider);
            db.SaveChanges();

            if (provider == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var provider = getItemById(id);

            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        [HttpPost]
        public ActionResult Edit(Provider provider)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            db.Entry(provider).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
