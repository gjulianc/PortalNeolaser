using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PortalNeolaser.Models;
using PortalNeolaser.Areas.Mobile.Models;
using System.IO;

namespace PortalNeolaser.Areas.Mobile.Controllers
{
    public class ElementoAuditadoController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();

        // GET: Mobile/ElementoAuditado
        public ActionResult Index(int idAuditoria,bool? esNueva)
        {
            //Obtiene los elementos auditados de la auditoria con id => idAuditoria
           
            var elementosAuditados = db.ElementosAuditados.Include(e => e.Auditoria).Include(e => e.Elemento).Where(e=>e.FkAuditoria == idAuditoria);
            ViewBag.IdAuditoria = idAuditoria;           
            ViewBag.Estado = esNueva;
            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega a los elementos auditados de la Audioria {2}.", DateTime.Now, User.Identity.Name, idAuditoria));
            return View(elementosAuditados.ToList());
        }
 
        // GET: Mobile/ElementoAuditado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElementosAuditado elementosAuditado = db.ElementosAuditados.Find(id);
            FormElementoAuditado viewmodel = new FormElementoAuditado();
            viewmodel.Id = elementosAuditado.Id;
            viewmodel.Estado = elementosAuditado.Estado;
            viewmodel.Descripcion = elementosAuditado.Descripcion;
            viewmodel.FkAuditoria = elementosAuditado.FkAuditoria;
            viewmodel.FkElemento = elementosAuditado.FkElemento;
            viewmodel.Observaciones = elementosAuditado.Observaciones;
            

            if (elementosAuditado == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAuditoria = elementosAuditado.FkAuditoria;
            ViewBag.FkAuditoria = new SelectList(db.Auditorias, "Id", "Id", elementosAuditado.FkAuditoria);
            ViewBag.FkElemento = new SelectList(db.Elementos, "Id", "Nombre", elementosAuditado.FkElemento);
            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega a editar elemento auditable.", DateTime.Now, User.Identity.Name));
            return View(viewmodel);
        }
       
        [HttpPost]        
        public ActionResult Edit(FormElementoAuditado model)
        {
            int tamaño = Request.ContentLength;
            if (ModelState.IsValid || Request.ContentLength <= 4096)
            {
                if (model.Foto != null && model.Foto.ContentLength > 0)
                {

                    var filename = Path.GetFileName(model.Foto.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/uploads/fotos_auditorias"), filename);
                    model.Foto.SaveAs(path);
                }
           
                ElementosAuditado e = new ElementosAuditado();
                e.Id = model.Id;
                e.Estado = model.Estado;
                e.Descripcion = model.Descripcion;
                if (model.Foto != null)
                    e.Foto = model.Foto.FileName; 
                else
                    e.Foto = "unknown_photo.png"; 
                e.FkElemento = model.FkElemento;
                e.FkAuditoria = model.FkAuditoria;
                e.Observaciones = model.Observaciones;
            

                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
                MvcApplication.Log.WriteLog(String.Format("{0};Acceso Base Datos;{1};Actualiza Elemento con Id {2}.", DateTime.Now, User.Identity.Name, e.Id));
                return RedirectToAction("Index", new { idAuditoria = e.FkAuditoria });
            }
            ViewBag.FkAuditoria = model.FkAuditoria;
            ViewBag.FkElemento = model.FkElemento;
            return View(model);
        }
    
        public ActionResult FinalizarAuditoria(int idAuditoria)
        {
            //actualizar la auditoria a estado = true
            try
            {
                Auditoria a = new Auditoria();
                a = db.Auditorias.Find(idAuditoria);
                a.Estado = true;
                db.SaveChanges();
                MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Finaliza Auditoría la Audioria {2}.", DateTime.Now, User.Identity.Name, idAuditoria));
            }
            catch
            {
                
            }   
            return RedirectToAction("Finalizar", new { id = idAuditoria });
        }

        public ActionResult Finalizar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            return View();
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
