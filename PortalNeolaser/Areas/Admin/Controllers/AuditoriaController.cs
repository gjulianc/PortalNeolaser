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
using DevExpress.Web.Mvc;
using System.Data.SqlClient;

namespace PortalNeolaser.Areas.Admin.Controllers
{
    public class AuditoriaController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();

        // GET: Admin/Auditoria
        public ActionResult Index()
        {
            var auditorias = db.Auditorias.Include(a => a.Sucursal).Include(a => a.Usuario);
            return View(auditorias.ToList());
        }

        // GET: Admin/Auditoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auditoria auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            return View(auditoria);
        }

        // GET: Admin/Auditoria/Create
        public ActionResult Create()
        {
            ViewBag.FkSucursal = new SelectList(db.Sucursals, "Id", "CodigoSAP");
            ViewBag.FkUsuario = new SelectList(db.Usuarios, "UserId", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([ModelBinder(typeof(DevExpressEditorsBinder))] AuditoriaViewModel item)
        {
            if (ModelState.IsValid)
            {
                Auditoria auditoria = new Auditoria
                {
                    FechaInicio = item.FechaInicio,
                    FechaFin = item.FechaFin,
                    FkSucursal = item.Sucursal,
                    FkUsuario = item.Usuario
                };
                db.Auditorias.Add(auditoria);
                db.SaveChanges();
                return RedirectToAction("NewAuditoria", auditoria);

            }

            ViewBag.FkSucursal = new SelectList(db.Sucursals, "Id", "CodigoSAP", item.Sucursal);
            ViewBag.FkUsuario = new SelectList(db.Usuarios, "UserId", "UserName", item.Usuario);
            return View(item);
        }

        // GET: Admin/Auditoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auditoria auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.FkSucursal = new SelectList(db.Sucursals, "Id", "CodigoSAP", auditoria.FkSucursal);
            ViewBag.FkUsuario = new SelectList(db.Usuarios, "UserId", "UserName", auditoria.FkUsuario);
            return View(auditoria);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaInicio,FechaFin,FkUsuario,FkSucursal")] Auditoria auditoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auditoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FkSucursal = new SelectList(db.Sucursals, "Id", "CodigoSAP", auditoria.FkSucursal);
            ViewBag.FkUsuario = new SelectList(db.Usuarios, "UserId", "UserName", auditoria.FkUsuario);
            return View(auditoria);
        }

        // GET: Admin/Auditoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auditoria auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            return View(auditoria);
        }

        // POST: Admin/Auditoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Auditoria auditoria = db.Auditorias.Find(id);
            db.Auditorias.Remove(auditoria);
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
        public ActionResult GridViewAuditoriaPartial()
        {
            var model = db.Auditorias.Include(a => a.Sucursal).Include(a => a.Usuario);
            return PartialView("_GridViewAuditoriaPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewAuditoriaPartialAddNew(PortalNeolaser.Models.Auditoria item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewAuditoriaPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewAuditoriaPartialUpdate(PortalNeolaser.Models.Auditoria item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewAuditoriaPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewAuditoriaPartialDelete(System.Int32 Id)
        {
            var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewAuditoriaPartial", model);
        }

        public ActionResult GetElementosByAuditoria(Auditoria AuditoriaID)
        {            
            return RedirectToAction("Index", "ElementoAuditado", new { idAuditoria = AuditoriaID.Id });
        }

        //Comienzo de Auditoria
        public ActionResult NewAuditoria(Auditoria auditoria)
        {
            List<ElementosAuditado> listaElementos = new List<ElementosAuditado>();
            string cadenaConexion = (@"server=82.98.161.118;user id = qualimove;password=mzprAh1r;database = neolaserdb");
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            SqlCommand cmd = new SqlCommand("spElementosAuditarBySucursal", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@paramID", SqlDbType.Int);
            cmd.Parameters["@paramID"].Value = auditoria.FkSucursal;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var item = new ElementosAuditado();
                item.FkAuditoria = auditoria.Id;
                item.FkElemento = (int)reader[0];
                item.Descripcion = reader[1].ToString();

                listaElementos.Add(item);
                db.ElementosAuditados.Add(item);
            }
            try
            {
                db.SaveChanges();
            }
            catch { }

            return RedirectToAction("Index", "ElementoAuditado", new { IdAuditoria = auditoria.Id });
        }

       
    }
}
