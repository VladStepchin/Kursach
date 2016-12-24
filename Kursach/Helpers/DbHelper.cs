using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kursach.Helpers
{
    public interface IDbHelper <T>
    {
        T getItemById(int? id);
    }
}