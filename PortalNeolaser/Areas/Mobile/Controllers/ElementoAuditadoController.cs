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

namespace PortalNeolaser.Areas.Mobile.Controllers
{
    public class ElementoAuditadoController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();

        // GET: Mobile/ElementoAuditado
        public ActionResult Index(int idAuditoria)
        {
            var elementosAuditados = db.ElementosAuditados.Include(e => e.Auditoria).Include(e => e.Elemento).Where(e=>e.FkAuditoria == idAuditoria);
            return View(elementosAuditados.ToList());
        }

        // GET: Mobile/ElementoAuditado/Details/5
        public ActionResult Details(int? id)
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
            return View(elementosAuditado);
        }

        // GET: Mobile/ElementoAuditado/Create
        public ActionResult Create()
        {
            ViewBag.FkAuditoria = new SelectList(db.Auditorias, "Id", "Id");
            ViewBag.FkElemento = new SelectList(db.Elementos, "Id", "Nombre");
            return View();
        }

        // POST: Mobile/ElementoAuditado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Estado,Descripcion,Foto,FkAuditoria,FkElemento")] ElementoAuditadoViewModel item)
        {
            ElementosAuditado e = new ElementosAuditado
            {
                Id = item.Id,
                Descripcion = item.Descripcion,
                Estado = item.Estado
                //Foto = item.Foto,
                //FkAuditoria = item.FkAuditoria,
                //FkElemento = item.FkElemento
            };
            if (ModelState.IsValid)
            {
                db.ElementosAuditados.Add(e);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.FkAuditoria = new SelectList(db.Auditorias, "Id", "Id", item.FkAuditoria);
            //ViewBag.FkElemento = new SelectList(db.Elementos, "Id", "Nombre", item.FkElemento);
            return View(item);
        }

        // GET: Mobile/ElementoAuditado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElementosAuditado elementosAuditado = db.ElementosAuditados.Find(id);
            ElementoAuditadoViewModel viewmodel = new ElementoAuditadoViewModel();
            viewmodel.Id = elementosAuditado.Id;
            viewmodel.Estado = elementosAuditado.Estado;
            viewmodel.Descripcion = elementosAuditado.Descripcion;
            
            if (elementosAuditado == null)
            {
                return HttpNotFound();
            }
            ViewBag.FkAuditoria = new SelectList(db.Auditorias, "Id", "Id", elementosAuditado.FkAuditoria);
            ViewBag.FkElemento = new SelectList(db.Elementos, "Id", "Nombre", elementosAuditado.FkElemento);
            return View(viewmodel);
        }

        // POST: Mobile/ElementoAuditado/Edit/5 ,Foto,FkAuditoria,FkElemento
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Estado,Descripcion")] ElementoAuditadoViewModel item)
        {
            if (ModelState.IsValid)
            {
                ElementosAuditado e = new ElementosAuditado();
                e.Id = item.Id;
                e.Estado = item.Estado;
                //e.Foto = item.Foto;
                //e.FkElemento = item.FkElemento;
                //e.FkAuditoria = item.FkAuditoria;

                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.FkAuditoria = new SelectList(db.Auditorias, "Id", "Id", item.FkAuditoria);
            //ViewBag.FkElemento = new SelectList(db.Elementos, "Id", "Nombre", item.FkElemento);
            return View(item);
        }

        // GET: Mobile/ElementoAuditado/Delete/5
        public ActionResult Delete(int? id)
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
            return View(elementosAuditado);
        }

        // POST: Mobile/ElementoAuditado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ElementosAuditado elementosAuditado = db.ElementosAuditados.Find(id);
            db.ElementosAuditados.Remove(elementosAuditado);
            db.SaveChanges();
            return RedirectToAction("Index");
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
