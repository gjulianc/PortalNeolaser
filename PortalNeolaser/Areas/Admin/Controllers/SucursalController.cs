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
    public class SucursalController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();

        // GET: Admin/Sucursal
        public ActionResult Index()
        {
            var sucursals = db.Sucursals.Include(s => s.Cliente);
            return View(sucursals.ToList());
        }

        // GET: Admin/Sucursal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursal sucursal = db.Sucursals.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        

        // GET: Admin/Sucursal/Create
        public ActionResult Create()
        {
            ViewBag.Comunidad = new SelectList(db.comunidades, "nombre", "nombre");
            ViewBag.Provincia = new SelectList(db.provincias, "nombre", "nombre");
            ViewBag.Municipio = new SelectList(db.municipios, "nombre", "nombre");
            ViewBag.FkCliente = new SelectList(db.Clientes, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([ModelBinder(typeof(DevExpressEditorsBinder))] SucursalViewModel item)
        {
            if (ModelState.IsValid)
            {
                Sucursal sucursal = new Sucursal
                {
                    CodigoSAP = item.CodigoSAP,
                    Descripcion = item.Descripcion,
                    FechaAlta = item.FechaAlta,
                    Direccion = item.Direccion,
                    Poblacion = item.Poblacion,
                    Provincia = item.Provincia,
                    ComunidadAutonoma = item.ComunidadAutonoma,
                    CodPostal = item.CodPostal,
                    FkCliente = item.Cliente
                };
                db.Sucursals.Add(sucursal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FkCliente = new SelectList(db.Clientes, "Id", "Nombre", item.Cliente);
            return View(item);
        }

        // GET: Admin/Sucursal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursal sucursal = db.Sucursals.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            ViewBag.FkCliente = new SelectList(db.Clientes, "Id", "Nombre", sucursal.FkCliente);
            return View(sucursal);
        }

        // POST: Admin/Sucursal/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodigoSAP,Descripcion,Direccion,Poblacion,Provincia,CodPostal,FkCliente")] Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sucursal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FkCliente = new SelectList(db.Clientes, "Id", "Nombre", sucursal.FkCliente);
            return View(sucursal);
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
        public ActionResult GridViewSucursalesPartial()
        {
            var model = db.Sucursals;
            return PartialView("_GridViewSucursalesPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewSucursalesPartialUpdate(PortalNeolaser.Models.Sucursal item)
        {
            var model = db.Sucursals;
            if (ModelState.IsValid)
            {
                
                try
                {
                    if (ModelState.IsValid)
                    {
                        var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                        try
                        {
                            this.UpdateModel(modelItem);
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            ViewData["EditError"] = e.Message;
                        }
                    }
                    else
                        ViewData["EditError"] = "Por favor, corrija todos los errores.";
                    return PartialView("_GridViewSucursalesPartial", model.ToList());
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewSucursalesPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewSucursalesPartialDelete(System.Int32 Id)
        {
            var model = db.Sucursals;
            if (Id >= 0)
            {
                try
                {
                    Sucursal sucursal = db.Sucursals.Find(Id);
                    db.Sucursals.Remove(sucursal);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewSucursalesPartial", model);
        }

        public ActionResult GetGroupsBySucursal(int id)
        {
            return RedirectToAction("Index", "GrupoElementos", new { id = id });
        }
    }
}
