using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalNeolaser.Areas.Client.Controllers
{
    public class AuditoriasController : Controller
    {
        // GET: Client/Auditorias
        public ActionResult Index()
        {
            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega a Auditorias.", DateTime.Now));
            return View();
        }
    }
}