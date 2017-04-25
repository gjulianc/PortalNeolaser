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
        public ActionResult Index(int? idAuditoria)
        {
            if (idAuditoria != null)
            {
                Session["AuditoriaID"] = idAuditoria;
                Auditoria auditoria = new Auditoria();
                auditoria = db.Auditorias.Find(idAuditoria);
                TimeSpan ts = (DateTime)auditoria.FechaFin - (DateTime)auditoria.FechaInicio;
                //= db.Auditorias.Include(a => a.Sucursal).Include(a => a.Usuario).Where(a=>a.Id == idAuditoria) as Auditoria;
                ViewData["Empleado"] = auditoria.Usuario.UserName;
                ViewData["Cliente"] = auditoria.Sucursal.Cliente.Nombre;
                ViewData["ComunidadAutonoma"] = auditoria.Sucursal.ComunidadAutonoma;
                ViewData["Provincia"] = auditoria.Sucursal.Provincia;
                ViewData["Poblacion"] = auditoria.Sucursal.Poblacion;
                ViewData["Direccion"] = auditoria.Sucursal.Direccion;
                ViewData["CP"] = auditoria.Sucursal.CodPostal;
                ViewData["CodigoSAP"] = auditoria.Sucursal.CodigoSAP;
                ViewData["Inicio"] = auditoria.FechaInicio;
                ViewData["Fin"] = auditoria.FechaFin;
                ViewData["Duracion"] = ts.Duration();
            }
            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega a los Elementos Auditados con incidencias.", DateTime.Now, User.Identity.Name));
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewElementoAuditadoPartial(int? idAuditoria)
        {
            ViewData["AuditoriaID"] = idAuditoria;
            var model = db.ElementosAuditados.Where(m => m.FkAuditoria == idAuditoria);
            return PartialView("_GridViewElementoAuditadoPartial", model.ToList());
        }

        public ActionResult ElementosEnMalEstado()
        {
            return View();
        }   

        [ValidateInput(false)]
        public ActionResult GridViewElementosMalPartial()
        {
            Usuario u = new Usuario();
            u = db.Usuarios.FirstOrDefault(m => m.UserName == User.Identity.Name);

            var model = db.ElementosAuditados.Where(m => m.Estado == false && m.Auditoria.Sucursal.FkCliente == u.FkCliente);
            return PartialView("_GridViewElementosMalPartial", model.ToList());
        }
    }
}