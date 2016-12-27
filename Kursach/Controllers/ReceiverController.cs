using Kursach.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kursach.Controllers
{
    public class ReceiverController : Controller, IDbHelper<Receiver>
    {

        MyDb db = new MyDb();

        public Receiver getItemById(int? id)
        {
            var listofSelectedReceivers = db.Receivers.ToList().Where(x => x.Id == id);

            return listofSelectedReceivers.FirstOrDefault();
        }

        public ActionResult Index()
        {
            List<Receiver> Receivers = db.Receivers.ToList();

            return View(Receivers);
        }

        public ActionResult ProviderReceiverInterconnection()
        {
            List<ProviderReceiverInterconnection> ProviderReceiverInterconnection = db.ProviderReceiverInterconnections.ToList();

            return View(ProviderReceiverInterconnection);
        }

       [HttpPost]
        public ActionResult Create(Receiver receiver)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            db.Receivers.Add(receiver);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var receiver = getItemById(id);

            if (receiver == null)
            {
                return HttpNotFound();
            }

            return PartialView("Delete", receiver);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeletConfirm(int? id)
        {
            var receiver = getItemById(id);

            db.Receivers.Remove(receiver);
            db.SaveChanges();

            if (receiver == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var receiver = getItemById(id);

            if (receiver == null)
            {
                return HttpNotFound();
            }
            return View(receiver);
        }

        [HttpPost]
        public ActionResult Edit(Receiver receiver)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            db.Entry(receiver).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
