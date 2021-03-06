﻿using DevExpress.Web.Mvc;
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
using DevExpress.Web;
using System.Web.UI;
using System.IO;

namespace PortalNeolaser.Areas.Admin.Controllers
{
    public class ElementoAuditadoController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();
        private List<ElementosAuditado> elementosAuditados = new List<ElementosAuditado>();

        // GET: Admin/ElementoAuditado
        public ActionResult Index(int idAuditoria)
        {
            Session["AuditoriaID"] = idAuditoria;
            PortalNeolaser.Models.Auditoria auditoria = new PortalNeolaser.Models.Auditoria();
            auditoria = db.Auditorias.Find(idAuditoria);
            TimeSpan ts = (DateTime)auditoria.FechaFin - (DateTime)auditoria.FechaInicio;
            //= db.Auditorias.Include(a => a.Sucursal).Include(a => a.Usuario).Where(a=>a.Id == idAuditoria) as Auditoria;
            ViewData["Empleado"] = auditoria.Usuario.UserName;
            ViewData["Cliente"] = auditoria.Sucursal.Cliente.Nombre;
            ViewData["ComunidadAutonoma"] = auditoria.Sucursal.ComunidadAutonoma;
            ViewData["Provincia"] = auditoria.Sucursal.Provincia;
            ViewData["Poblacion"] = auditoria.Sucursal.Poblacion;
            ViewData["Direccion"] = auditoria.Sucursal.Direccion;
            ViewData["CP"] = auditoria.Sucursal.CodPostal;
            ViewData["CodigoSAP"] = auditoria.Sucursal.CodigoSAP;
            ViewData["Inicio"] = auditoria.FechaInicio;
            ViewData["Fin"] = auditoria.FechaFin;
            ViewData["Duracion"] = ts.Duration();
            ViewData["Estado"] = auditoria.Estado;
            MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega Comienzo de Auditoria ID {2}", DateTime.Now, User.Identity.Name,idAuditoria)); //Escribimos en el log
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewElementosAuditadosPartial()
        {
            int auditoria = (int)Session["AuditoriaID"];
            var model = db.ElementosAuditados.Include(e => e.Auditoria).Include(e => e.Elemento).Where(e => e.FkAuditoria == auditoria);
            return PartialView("_GridViewElementosAuditadosPartial", model.ToList());
        }

        // GET: Admin/ElementoAuditado/Edit/5
        public ActionResult Edit(int? id)
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
            ElementoAuditadoViewModel e = new Models.ElementoAuditadoViewModel()
            {
                Id = elementosAuditado.Id,
                Elemento = elementosAuditado.Elemento.Nombre,
                Descripcion = elementosAuditado.Elemento.Descripcion,
                Estado = elementosAuditado.Estado ?? false,
                FkAuditoria = elementosAuditado.FkAuditoria,
                FkElemento = elementosAuditado.FkElemento,
                Observaciones = elementosAuditado.Observaciones
                //Foto = elementosAuditado.Foto
            };
            ViewBag.FkAuditoria = new SelectList(db.Auditorias, "Id", "Id", elementosAuditado.FkAuditoria);
            ViewBag.FkElemento = new SelectList(db.Elementos, "Id", "Nombre", elementosAuditado.FkElemento);
            return View(e);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Elemento,Descripcion,Estado,Foto,FkAuditoria,FkElemento,Observaciones")] ElementoAuditadoViewModel item)
        {
            string fotoFileName = string.Empty;
            var model = db.ElementosAuditados;
            var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
       
            if (ModelState.IsValid)
            {
                if (item.Foto.Length > 0 && item.Foto[0].ContentLength > 0)
                {
                    fotoFileName = string.Format("~/Content/uploads/fotos_auditorias/{0}", item.Foto[0].FileName);
                    item.Foto[0].SaveAs(Server.MapPath(fotoFileName));
                    modelItem.Foto = item.Foto[0].FileName;
                }               
                modelItem.Estado = item.Estado;
                modelItem.FkAuditoria = item.FkAuditoria;
                modelItem.FkElemento = item.FkElemento;
                modelItem.Observaciones = item.Observaciones;
                db.Entry(modelItem).State = EntityState.Modified;
                db.SaveChanges();
                MvcApplication.Log.WriteLog(String.Format("{0};Acceso Base Datos;{1};Actualiza Elemento Auditado", DateTime.Now, User.Identity.Name)); //Escribimos en el log
                return RedirectToAction("Index", new { idAuditoria = modelItem.FkAuditoria });
            }
            
            ViewBag.FkAuditoria = new SelectList(db.Auditorias, "Id", "Id", item.FkAuditoria);
            ViewBag.FkElemento = new SelectList(db.Elementos, "Id", "Nombre", item.FkElemento);
            return View(item);   
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewElementosAuditadosPartialUpdate([Bind(Include = "Id,Descripcion,Estado,Foto,FkAuditoria,FkElemento,Observaciones")] ElementoAuditadoViewModel item)
        {
            var model = db.GruposElementos;
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
                            MvcApplication.Log.WriteLog(String.Format("{0};Acceso Base Datos;{1};Acutaliza Sucursal ID: {2}", DateTime.Now, User.Identity.Name, item.Id)); //Escribimos en el log
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



