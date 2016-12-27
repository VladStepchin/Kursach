using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kursach.ViewModels
{
    public class ScrapViewModel
    {
        MyDb db = new MyDb();

        public SelectList Title { get; set; }
    }
}