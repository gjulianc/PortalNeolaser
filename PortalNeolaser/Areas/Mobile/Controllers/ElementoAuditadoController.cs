﻿using System;
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
        public ActionResult Index(int idAuditoria)
        {
            //Obtiene los elementos auditados de la auditoria con id => idAuditoria
           
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
            viewmodel.FkAuditoria = elementosAuditado.FkAuditoria;
            viewmodel.FkElemento = elementosAuditado.FkElemento;


            if (elementosAuditado == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAuditoria = elementosAuditado.FkAuditoria;
            ViewBag.FkAuditoria = new SelectList(db.Auditorias, "Id", "Id", elementosAuditado.FkAuditoria);
            ViewBag.FkElemento = new SelectList(db.Elementos, "Id", "Nombre", elementosAuditado.FkElemento);
            return View(viewmodel);
        }

        // POST: Mobile/ElementoAuditado/Edit/5  [Bind(Include = "Id,Estado,Descripcion")]
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ElementoAuditadoViewModel model)
        {
            if (ModelState.IsValid)
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
                e.Foto = model.Foto.FileName;
                e.FkElemento = model.FkElemento;
                e.FkAuditoria = model.FkAuditoria;

                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { idAuditoria = e.FkAuditoria });
            }
            ViewBag.FkAuditoria = model.FkAuditoria;
            ViewBag.FkElemento = model.FkElemento;
            return View(model);
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
