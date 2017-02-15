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
            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega al Dashboard.", DateTime.Now));
            return View();
        }
    }
}