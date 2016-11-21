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
using WebMatrix.WebData;
using System.Web.Security;

namespace PortalNeolaser.Areas.Admin.Controllers
{
    public class UsuarioController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();

        // GET: Admin/Usuario
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Cliente);
            return View(usuarios.ToList());
        }

        // GET: Admin/Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Admin/Usuario/Create
        public ActionResult Create()
        {
            ViewBag.FkCliente = new SelectList(db.Clientes, "Id", "Nombre");
            ViewBag.FkRol = new SelectList(db.webpages_Roles, "RoleName", "RoleName");

            return View();
        }

        // POST: Admin/Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([ModelBinder(typeof(DevExpressEditorsBinder))] UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {                
                WebSecurity.CreateUserAndAccount(usuario.UserName, usuario.Password, new {Email = usuario.Email, FkCliente = usuario.FkCliente}, false);
                Roles.AddUserToRole(usuario.UserName, usuario.Rol);
                return RedirectToAction("Index");
            }

            ViewBag.FkCliente = new SelectList(db.Clientes, "Id", "Nombre", usuario.FkCliente);
            ViewBag.FkRol = new SelectList(db.webpages_Roles, "RoleName", "RoleName");
            return View(usuario);
        }

        

       
        // GET: Admin/Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Admin/Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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
        public ActionResult GridViewUsuariosPartial()
        {
            var model = db.Usuarios;
            return PartialView("_GridViewUsuariosPartial", model.ToList());
        }

        
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewUsuariosPartialUpdate(Usuario item)
        {
            var model = db.Usuarios;
            
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.UserName == item.UserName);

                    if (modelItem != null)
                    {
                      
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewUsuariosPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewUsuariosPartialDelete(System.Int32 UserId)
        {
            var model = db.Usuarios;
            if (UserId >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.UserId == UserId);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewUsuariosPartial", model.ToList());
        }
       
    }
}
