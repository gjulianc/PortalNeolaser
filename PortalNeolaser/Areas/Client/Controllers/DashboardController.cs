using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalNeolaser.Areas.Client.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Client/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}