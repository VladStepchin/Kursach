using HiQPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kursach.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        ScrapMetal db = new ScrapMetal();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
