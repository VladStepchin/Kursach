using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kursach.Helpers;

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
