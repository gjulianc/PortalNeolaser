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
    public class ClienteController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();

        // GET: Admin/Cliente
        public ActionResult Index()
        {
            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega a Clientes", DateTime.Now, User.Identity.Name)); //Escribimos en el log
            return View(db.Clientes.ToList());
        }

        // GET: Admin/Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Admin/Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cliente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([ModelBinder(typeof(DevExpressEditorsBinder))] ClienteViewModel item)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = new Cliente { Nombre = item.Nombre, Cif = item.Cif, CodPostal = item.CodPostal, Direccion = item.Direccion, Email = item.Email
                , Poblacion = item.Poblacion, Provincia = item.Provincia, Telefono = item.Telefono};

                db.Clientes.Add(cliente);
                db.SaveChanges();
                MvcApplication.Log.WriteLog(String.Format("{0};Acceso Base Datos;{1};Crea el Cliente {2}", DateTime.Now, User.Identity.Name, item.Cif)); //Escribimos en el log
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Admin/Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Admin/Cliente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cif,Nombre,Email,Telefono,Direccion,Poblacion,Provincia,CodPostal")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Admin/Cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Admin/Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
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
        public ActionResult GridViewClientePartial()
        {
            var model = db.Clientes;
            return PartialView("_GridViewClientePartial", model.ToList());
        }

        
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewClientePartialUpdate(PortalNeolaser.Models.Cliente item)
        {
            var model = db.Clientes;
            if (ModelState.IsValid)
            {
                var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                try
                {
                    this.UpdateModel(modelItem);
                    db.SaveChanges();
                    MvcApplication.Log.WriteLog(String.Format("{0};Acceso Base Datos;{1};Actualiza el Cliente {2}", DateTime.Now, User.Identity.Name, item.Id)); //Escribimos en el log
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewClientePartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewClientePartialDelete(System.Int32 Id)
        {
            var model = db.Clientes;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                    MvcApplication.Log.WriteLog(String.Format("{0};Acceso Base Datos;{1};Elimina el Cliente {2}", DateTime.Now, User.Identity.Name, Id)); //Escribimos en el log
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewClientePartial", model.ToList());
        }
    }
}
