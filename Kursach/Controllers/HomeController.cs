using HiQPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Kursach.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        MyDb db = new MyDb();

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
