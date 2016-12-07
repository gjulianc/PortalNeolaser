using DevExpress.Web.Mvc;
using PortalNeolaser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalNeolaser.Areas.Client.Controllers
{
    public class SucursalesController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();

        // GET: Client/Sucursales
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewSucursalesPartial()
        {
            Usuario u = new Usuario();
            u = db.Usuarios.FirstOrDefault(m => m.UserName == User.Identity.Name);            
            var model = db.Sucursals.Where(s => s.FkCliente == u.FkCliente );
            return PartialView("_GridViewSucursalesPartial", model.ToList());
        }


        [ValidateInput(false)]
        public ActionResult GridViewAuditoriaDetailPartial(int sucursalID)
        {
            ViewData["SucursalID"] = sucursalID;
            var model =  db.Auditorias.Where(m => m.FkSucursal == sucursalID);
            return PartialView("_GridViewAuditoriaDetailPartial", model.ToList());
        }
    }
}