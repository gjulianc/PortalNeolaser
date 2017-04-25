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
    public class GrupoElementosController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();

        // GET: Admin/GrupoElementos
        public ActionResult Index(int? id)
        {
            
            if (id != null)
            {
                ViewBag.Titulo = "Listado de grupos asignados a la sucursal " + id;
                MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega a Grupo Elementos Asignados a Sucursal", DateTime.Now, User.Identity.Name)); //Escribimos en el log
                var model  = from g in db.GruposElementos
                             where g.Surcusales_Grupos.Any(c=>c.FkSucursal == id) /*.Any(c => c.Category_ID == cat_id)*/
                             select g;
                return View(model.ToList());
            }                
            else
            {
                ViewBag.Titulo = "Listado de grupos";
                MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega a Listado de Grupos", DateTime.Now, User.Identity.Name)); //Escribimos en el log
                return View(db.GruposElementos.ToList());
            }
                
        }

        // GET: Admin/GrupoElementos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruposElemento gruposElemento = db.GruposElementos.Find(id);
            if (gruposElemento == null)
            {
                return HttpNotFound();
            }
            return View(gruposElemento);
        }

        // GET: Admin/GrupoElementos/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([ModelBinder(typeof(DevExpressEditorsBinder))] GrupoElementosViewModel item)
        {
            if (ModelState.IsValid)
            {
                GruposElemento grupo = new GruposElemento { Nombre = item.Nombre, Descripcion = item.Descripcion, Orden = item.Orden };
                db.GruposElementos.Add(grupo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Admin/GrupoElementos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruposElemento gruposElemento = db.GruposElementos.Find(id);
            if (gruposElemento == null)
            {
                return HttpNotFound();
            }
            return View(gruposElemento);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Descripcion")] GruposElemento gruposElemento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gruposElemento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gruposElemento);
        }

        // GET: Admin/GrupoElementos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruposElemento gruposElemento = db.GruposElementos.Find(id);
            if (gruposElemento == null)
            {
                return HttpNotFound();
            }
            return View(gruposElemento);
        }

        // POST: Admin/GrupoElementos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GruposElemento gruposElemento = db.GruposElementos.Find(id);
            db.GruposElementos.Remove(gruposElemento);
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
        public ActionResult GridViewGrupoElementosPartial()
        {
            var model = db.GruposElementos;
            return PartialView("_GridViewGrupoElementosPartial", model.ToList());
        }
        
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewGrupoElementosPartialUpdate(PortalNeolaser.Models.GruposElemento item)
        {
            var model = db.GruposElementos;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);

                    if (modelItem != null)
                    {

                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                        MvcApplication.Log.WriteLog(String.Format("{0};Actualziación Base Datos;{1};Actualiza el Grupo {2}", DateTime.Now, User.Identity.Name, item.Id)); //Escribimos en el log
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewGrupoElementosPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewGrupoElementosPartialDelete(System.Int32 Id)
        {
            var model = db.GruposElementos;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                    MvcApplication.Log.WriteLog(String.Format("{0};Actualización Base Datos;{1};Elimina elemento {2}", DateTime.Now, User.Identity.Name, Id)); //Escribimos en el log
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewGrupoElementosPartial", model.ToList());
        }
    }
}
