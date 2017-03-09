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

namespace PortalNeolaser.Areas.Admin.Controllers
{
    public class ElementoController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();

        // GET: Admin/Elemento
        public ActionResult Index()
        {
            var elementos = db.Elementos.Include(e => e.GruposElemento);
            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega Elementos", DateTime.Now, User.Identity.Name)); //Escribimos en el log
            return View(elementos.ToList());
        }

        // GET: Admin/Elemento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elemento elemento = db.Elementos.Find(id);
            if (elemento == null)
            {
                return HttpNotFound();
            }
            return View(elemento);
        }

        // GET: Admin/Elemento/Create
        public ActionResult Create()
        {
            ViewBag.FkGrupo = new SelectList(db.GruposElementos, "Id", "Nombre");
            return View();
        }

        // POST: Admin/Elemento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([ModelBinder(typeof(DevExpressEditorsBinder))] ElementoViewModel item)
        {
            if (ModelState.IsValid)
            {
                Elemento elemento = new Elemento { Nombre = item.Nombre, Descripcion = item.Descripcion, Orden = item.Orden, FkGrupo = item.Grupo };
                db.Elementos.Add(elemento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FkGrupo = new SelectList(db.GruposElementos, "Id", "Nombre");
            return View(item);
        }

        // GET: Admin/Elemento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elemento elemento = db.Elementos.Find(id);
            if (elemento == null)
            {
                return HttpNotFound();
            }
            ViewBag.FkGrupo = new SelectList(db.GruposElementos, "Id", "Nombre", elemento.FkGrupo);
            return View(elemento);
        }

        // POST: Admin/Elemento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Descripcion,FkGrupo")] Elemento elemento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(elemento).State = EntityState.Modified;
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }
            ViewBag.FkGrupo = new SelectList(db.GruposElementos, "Id", "Nombre", elemento.FkGrupo);
            return View(elemento);
        }

        // GET: Admin/Elemento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elemento elemento = db.Elementos.Find(id);
            if (elemento == null)
            {
                return HttpNotFound();
            }
            return View(elemento);
        }

        // POST: Admin/Elemento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Elemento elemento = db.Elementos.Find(id);
            db.Elementos.Remove(elemento);
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


        [ValidateInput(false)]
        public ActionResult GridViewElementosPartial()
        {
            var model = db.Elementos;
            ViewBag.FkGrupo = new SelectList(db.GruposElementos, "Id", "Nombre");
            return PartialView("_GridViewElementosPartial", model.ToList());
        }

        
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewElementosPartialUpdate(PortalNeolaser.Models.Elemento item)
        {
            var model = db.Elementos;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                        MvcApplication.Log.WriteLog(String.Format("{0};Actualiza Base Datos;{1};Actualiza Elemento {2}", DateTime.Now, User.Identity.Name, item.Id)); //Escribimos en el log
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewElementosPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewElementosPartialDelete(System.Int32 Id)
        {
            var model = db.Elementos;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                    MvcApplication.Log.WriteLog(String.Format("{0};Actualiza Base Datos;{1};Elimina Elemento {2}", DateTime.Now, User.Identity.Name, Id)); //Escribimos en el log
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewElementosPartial", model.ToList());
        }
    }
}
