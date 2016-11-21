using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PortalNeolaser.Models;

namespace PortalNeolaser.Areas.Mobile.Controllers
{
    public class SucursalController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();

        // GET: Mobile/Sucursal
        public ActionResult Index()
        {
            var sucursals = db.Sucursals.Include(s => s.Cliente);
            return View(sucursals.ToList());
        }

        // GET: Mobile/Sucursal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursal sucursal = db.Sucursals.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
