using Kursach.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kursach.Controllers
{
    public class StoreController : Controller, IDbHelper<Store>
    {
        ScrapMetal db = new ScrapMetal();

        public Store getItemById(int? id)
        {
            var listofSelectedStores = db.Stores.ToList().Where(x => x.Id == id);

            return listofSelectedStores.FirstOrDefault();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Store> Stores = db.Stores.ToList();

            return View(Stores);
        }

        [HttpPost]
        public ActionResult Create(Store store)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            db.Stores.Add(store);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Scrap/Create

       [HttpGet]
        public ActionResult Delete(int? id)
        {
            var store = getItemById(id);

            if (store == null)
            {
                return HttpNotFound();
            }

            return PartialView("Delete", store);
        }

       [HttpPost, ActionName("Delete")]
       public ActionResult DeletConfirm(int? id)
       {

           var store = getItemById(id);

           db.Stores.Remove(store);
           db.SaveChanges();

           if (store == null)
           {
               return HttpNotFound();
           }

           return RedirectToAction("Index");
       }

       [HttpGet]
       public ActionResult Edit(int? id)
       {
           var store = getItemById(id);

           if (store == null)
           {
               return HttpNotFound();
           }
           return View(store);
       }

       [HttpPost]
       public ActionResult Edit(Store store)
       {
           if (!ModelState.IsValid)
           {
               return View("Error");
           }

           db.Entry(store).State = EntityState.Modified;
           db.SaveChanges();
           return RedirectToAction("Index");
       }
    }
}
