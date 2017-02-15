using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalNeolaser.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega al Dashboard de Administración", DateTime.Now, User.Identity.Name)); //Escribimos en el log
            return View();
        }
    }
}