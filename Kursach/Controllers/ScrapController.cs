using HiQPdf;
using Kursach.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kursach.Controllers
{
    public class ScrapController : Controller, IDbHelper<Scrap>
    {
        ScrapMetal db = new ScrapMetal();

        public Scrap getItemById(int? id) 
        {
            var listofSelectedMetal = db.Scraps.ToList().Where(x => x.Id == id);

            return listofSelectedMetal.FirstOrDefault();
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
         public ActionResult ConvertHtmlPageToPdf()
         {
             List<Scrap> Scraps = db.Scraps.ToList();

             // get the HTML code of this view
             string htmlToConvert = RenderViewAsString("LisOfMetalToPDF", Scraps);

             // the base URL to resolve relative images and css
             String thisPageUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
             String baseUrl = thisPageUrl.Substring(0, thisPageUrl.Length - 
                 "Home/ConvertThisPageToPdf".Length);

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

             return File(pdfBuffer, "application/pdf", "ThisMvcViewToPdf.pdf");
         } 

        [HttpGet]
        public ActionResult Index()
         {        
            List<Scrap> Scraps = db.Scraps.ToList();
            return View(Scraps);
        }

        [HttpPost]
        public ActionResult Create(Scrap metal)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            db.Scraps.Add(metal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Scrap/Create

       [HttpGet]
        public ActionResult Delete(int? id)
        {
            var metal = getItemById(id);

            if (metal == null)
            {
                return HttpNotFound();
            }

            return PartialView("Delete", metal);
        }

       [HttpPost, ActionName("Delete")]
       public ActionResult DeletConfirm(int? id)
       {

           var metal = getItemById(id);

           db.Scraps.Remove(metal);
           db.SaveChanges();

           if (metal == null)
           {
               return HttpNotFound();
           }

           return RedirectToAction("Index");
       }

       [HttpGet]
       public ActionResult Edit(int? id)
       {
           var metal = getItemById(id);

           if (metal == null)
           {
               return HttpNotFound();
           }
           return View(metal);
       }

       [HttpPost]
       public ActionResult Edit(Scrap metal)
       {
           if (!ModelState.IsValid)
           {
               return View("Error");
           }

           db.Entry(metal).State = EntityState.Modified;
           db.SaveChanges();
           return RedirectToAction("Index");
       }
    }
}
