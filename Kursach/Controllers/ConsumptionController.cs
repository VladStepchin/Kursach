using Kursach.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kursach.Controllers
{
    public class ConsumptionController : Controller, IDbHelper<Consumption>
    {

        MyDb db = new MyDb();

        public Consumption getItemById(int? id)
        {
            var listofSelectedConsumptions = db.Consumptions.ToList().Where(x => x.Id == id);

            return listofSelectedConsumptions.FirstOrDefault();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Consumption> Consumptions = db.Consumptions.ToList();

            return View(Consumptions);
        }

        [HttpPost]
        public ActionResult Create(Consumption consumption)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            db.Consumptions.Add(consumption);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       [HttpGet]
        public ActionResult Delete(int? id)
        {
            var consumption = getItemById(id);

            if (consumption == null)
            {
                return HttpNotFound();
            }

            return PartialView("Delete", consumption);
        }

       [HttpPost, ActionName("Delete")]
       public ActionResult DeletConfirm(int? id)
       {

           var consumption = getItemById(id);

           if (consumption == null)
           {
               return HttpNotFound();
           }

           db.Consumptions.Remove(consumption);
           db.SaveChanges();

           return RedirectToAction("Index");
       }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var consumption = getItemById(id);

            if (consumption == null)
            {
                return HttpNotFound();
            }
            return View(consumption);
        }

        [HttpPost]
        public ActionResult Edit(Consumption consumption)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            db.Entry(consumption).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
