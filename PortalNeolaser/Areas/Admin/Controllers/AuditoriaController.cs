using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            PortalNeolaser.Models.Auditoria auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            return View(auditoria);
        }

        // GET: Admin/Auditoria/Create
        public ActionResult Create()
        {
            Models.AuditoriaViewModel model = new Models.AuditoriaViewModel();
            model.FechaInicio = DateTime.Now;
            model.FechaFin = DateTime.Now;
            ViewBag.FkSucursal = new SelectList(db.Sucursals, "Id", "CodigoSAP");
            ViewBag.FkUsuario = new SelectList(db.Usuarios, "UserId", "UserName");
            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega a Nuevo Auditoria.", DateTime.Now, User.Identity.Name)); //Escribimos en el log
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([ModelBinder(typeof(DevExpressEditorsBinder))] Models.AuditoriaViewModel item)
        {
            if (ModelState.IsValid)
            {
                PortalNeolaser.Models.Auditoria auditoria = new PortalNeolaser.Models.Auditoria
                {
                    FechaInicio = item.FechaInicio,
                    FechaFin = item.FechaFin,
                    FkSucursal = item.Sucursal,
                    FkUsuario = item.Usuario,
                    Estado = false
                };
                db.Auditorias.Add(auditoria);
                db.SaveChanges();
                MvcApplication.Log.WriteLog(String.Format("{0};Inserción en Base Datos;{1};Inserta Nuevo Auditoria", DateTime.Now,auditoria.Id)); //Escribimos en el log
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
            PortalNeolaser.Models.Auditoria auditoria = db.Auditorias.Find(id);
            AuditoriaViewModel vm = new AuditoriaViewModel();
            vm.Id = auditoria.Id;
            vm.Estado = auditoria.Estado;
            vm.FechaFin = auditoria.FechaFin;
            vm.FechaInicio = auditoria.FechaInicio;
            vm.Sucursal = auditoria.Sucursal.Id;
            vm.Usuario = auditoria.Usuario.UserId;
            
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.FkSucursal = new SelectList(db.Sucursals, "Id", "CodigoSAP", auditoria.FkSucursal);
            ViewBag.FkUsuario = new SelectList(db.Usuarios, "UserId", "UserName", auditoria.FkUsuario);
            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Edita Auditoria {2}", DateTime.Now,id, User.Identity.Name,id)); //Escribimos en el log
            return View(vm);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([ModelBinder(typeof(DevExpressEditorsBinder))] Models.AuditoriaViewModel item)
        {
            if (ModelState.IsValid)
            {
                var auditoria = db.Auditorias.Find(item.Id);
                auditoria.FechaFin = item.FechaFin;
                auditoria.FechaInicio = item.FechaInicio;
                auditoria.FkSucursal = item.Sucursal;
                auditoria.FkUsuario = item.Usuario;
                auditoria.Estado = item.Estado;

                db.Entry(auditoria).State = EntityState.Modified;
                db.SaveChanges();
                MvcApplication.Log.WriteLog(String.Format("{0};Actualiza Base Datos;{1};Gurda Edición Auditoria", DateTime.Now,User.Identity.Name)); //Escribimos en el log
                return RedirectToAction("Index");
            }
            ViewBag.FkSucursal = new SelectList(db.Sucursals, "Id", "CodigoSAP", item.Sucursal);
            ViewBag.FkUsuario = new SelectList(db.Usuarios, "UserId", "UserName", item.Usuario);
            return View(item);
        }

        // GET: Admin/Auditoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortalNeolaser.Models.Auditoria auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Eliminacion Auditoria {2}", DateTime.Now, id)); //Escribimos en el log
            return View(auditoria);
        }

        // POST: Admin/Auditoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PortalNeolaser.Models.Auditoria auditoria = db.Auditorias.Find(id);
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
            
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    MvcApplication.Log.WriteLog(String.Format("{0};Actualiza Base Datos;{1};Guarda Edición Auditoria {2}", DateTime.Now, User.Identity.Name, item.Id)); //Escribimos en el log                    
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Por favor, debe corregir todos los errores.";
            return PartialView("_GridViewAuditoriaPartial"); // RedirectToAction("Index");
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

        public ActionResult GetElementosByAuditoria(PortalNeolaser.Models.Auditoria AuditoriaID)
        {            
            return RedirectToAction("Index", "ElementoAuditado", new { idAuditoria = AuditoriaID.Id });
        }

        //Comienzo de Auditoria
        public ActionResult NewAuditoria(PortalNeolaser.Models.Auditoria auditoria)
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
                MvcApplication.Log.WriteLog(String.Format("{0};Actualización Base Datos;{1};Guarda Elementos Auditados de la Nueva Auditoria {2}", DateTime.Now, User.Identity.Name, auditoria.Id)); //Escribimos en el log
            }
            catch { }

            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega a los elementos auditados de la Nueva Auditoria {2}", DateTime.Now, User.Identity.Name, auditoria.Id)); //Escribimos en el log
            return RedirectToAction("Index", "ElementoAuditado", new { IdAuditoria = auditoria.Id });
        }

        //Comienzo de Auditoria
        public ActionResult FinalizarAuditoria(int Id)
        {
            PortalNeolaser.Models.Auditoria auditoria = db.Auditorias.Find(Id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            else
            {
                auditoria.Estado = true;
                auditoria.FechaFin = DateTime.Now;
                try
                {
                    db.Entry(auditoria).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch { }
                MvcApplication.Log.WriteLog(String.Format("{0};Actualiza Base Datos;{1};Finaliza Auditoria", DateTime.Now, User.Identity.Name)); //Escribimos en el log
            }
            return View("Index");
        }
    }
}
