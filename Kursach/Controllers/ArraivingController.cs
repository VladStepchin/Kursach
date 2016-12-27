using Kursach.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kursach.Controllers
{
    public class ArraivingController : Controller, IDbHelper<Arraiving>
    {
        MyDb db = new MyDb();

        public Arraiving getItemById(int? id)
        {
            var listofSelectedArraivings = db.Arraivings.ToList().Where(x => x.Id == id);

            return listofSelectedArraivings.FirstOrDefault();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Arraiving> Arraivings = db.Arraivings.ToList();

            return View(Arraivings);
        }

        [HttpGet]
        public ActionResult GetDetailsOfArraivedMetal()
        {
            List<DetailsOfArraivedMetal> DetailsOfArraivedMetal = db.DetailsOfArraivedMetals.ToList();

            return View(DetailsOfArraivedMetal);
        }

        [HttpPost]
        public ActionResult Create(Arraiving arraiving)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            db.Arraivings.Add(arraiving);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Scrap/Create

       [HttpGet]
        public ActionResult Delete(int? id)
        {
            var arraiving = getItemById(id);

            if (arraiving == null)
            {
                return HttpNotFound();
            }

            return PartialView("Delete", arraiving);
        }

       [HttpPost, ActionName("Delete")]
       public ActionResult DeletConfirm(int? id)
       {

           var arraiving = getItemById(id);

           db.Arraivings.Remove(arraiving);
           db.SaveChanges();

           if (arraiving == null)
           {
               return HttpNotFound();
           }

           return RedirectToAction("Index");
       }

       [HttpGet]
       public ActionResult Edit(int? id)
       {
           var arraiving = getItemById(id);

           if (arraiving == null)
           {
               return HttpNotFound();
           }
           return View(arraiving);
       }

       [HttpPost]
       public ActionResult Edit(Arraiving arraiving)
       {
           if (!ModelState.IsValid)
           {
               return View("Error");
           }

           db.Entry(arraiving).State = EntityState.Modified;
           db.SaveChanges();
           return RedirectToAction("Index");
       }

       public ActionResult CountAllArraiving()
       {
           if (!ModelState.IsValid)
           {
               return View("Error");
           }

           db.Entry(arraiving).State = EntityState.Modified;
           db.SaveChanges();
           return RedirectToAction("Index");
       }
    }
}
