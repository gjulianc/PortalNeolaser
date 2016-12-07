using DevExpress.Web.Mvc;
using PortalNeolaser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalNeolaser.Areas.Client.Controllers
{
    public class ElementosAuditadosController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();

        // GET: Client/ElementosAuditados
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewElementoAuditadoPartial(int idAuditoria)
        {
            ViewData["AuditoriaID"] = idAuditoria;
            var model = db.ElementosAuditados.Where(m => m.FkAuditoria == idAuditoria);
            return PartialView("_GridViewElementoAuditadoPartial", model.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewElementoAuditadoErroneosPartial()
        {
            Usuario u = new Usuario();
            u = db.Usuarios.FirstOrDefault(m => m.UserName == User.Identity.Name);
            
            var model = db.ElementosAuditados.Where(m=>m.Estado == false && m.Auditoria.Sucursal.FkCliente == u.FkCliente );
            return PartialView("_GridViewElementoAuditadoPartial", model.ToList());
        }

    }
}