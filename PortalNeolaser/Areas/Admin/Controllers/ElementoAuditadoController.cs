using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PortalNeolaser.Models;
using PortalNeolaser.Areas.Admin.Models;
using DevExpress.Web;
using System.Web.UI;
using System.IO;

namespace PortalNeolaser.Areas.Admin.Controllers
{
    public class ElementoAuditadoController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();
        private List<ElementosAuditado> elementosAuditados = new List<ElementosAuditado>();

        // GET: Admin/ElementoAuditado
        public ActionResult Index(int idAuditoria)
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


            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewElementosAuditadosPartial()
        {
            int auditoria = (int)Session["AuditoriaID"];
            var model = db.ElementosAuditados.Include(e => e.Auditoria).Include(e => e.Elemento).Where(e => e.FkAuditoria == auditoria);
            return PartialView("_GridViewElementosAuditadosPartial", model.ToList());
        }

        // GET: Admin/ElementoAuditado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElementosAuditado elementosAuditado = db.ElementosAuditados.Find(id);
            if (elementosAuditado == null)
            {
                return HttpNotFound();
            }
            ElementoAuditadoViewModel e = new Models.ElementoAuditadoViewModel()
            {
                Id = elementosAuditado.Id,
                Descripcion = elementosAuditado.Descripcion,
                Estado = elementosAuditado.Estado,
                FkAuditoria = elementosAuditado.FkAuditoria,
                FkElemento = elementosAuditado.FkElemento,
                //Foto = elementosAuditado.Foto
            };
            ViewBag.FkAuditoria = new SelectList(db.Auditorias, "Id", "Id", elementosAuditado.FkAuditoria);
            ViewBag.FkElemento = new SelectList(db.Elementos, "Id", "Nombre", elementosAuditado.FkElemento);
            return View(e);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,Estado,Foto,FkAuditoria,FkElemento,Observaciones")] ElementoAuditadoViewModel item)
        {
            string fotoFileName = string.Empty;
            var model = db.ElementosAuditados;
            var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
       
            if (ModelState.IsValid)
            {
                if (item.Foto.Length > 0 && item.Foto[0].ContentLength > 0)
                {
                    fotoFileName = string.Format("~/Content/uploads/fotos_auditorias/{0}", item.Foto[0].FileName);
                    item.Foto[0].SaveAs(Server.MapPath(fotoFileName));
                }

                modelItem.Foto = item.Foto[0].FileName;
                modelItem.Estado = item.Estado;
                modelItem.FkAuditoria = item.FkAuditoria;
                modelItem.FkElemento = item.FkElemento;
                db.Entry(modelItem).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new { idAuditoria = modelItem.FkAuditoria });
            }
            
            ViewBag.FkAuditoria = new SelectList(db.Auditorias, "Id", "Id", item.FkAuditoria);
            ViewBag.FkElemento = new SelectList(db.Elementos, "Id", "Nombre", item.FkElemento);
            return View(item);   
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



