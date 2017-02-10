using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PortalNeolaser.Models;
using System.Data.SqlClient;

namespace PortalNeolaser.Areas.Mobile.Controllers
{
    public class AuditoriaController : Controller
    {
        private neolaserdbEntities db = new neolaserdbEntities();

        // GET: Mobile/Auditoria
        public ActionResult Index()
        {
            var auditorias = db.Auditorias.Include(a => a.Sucursal).Include(a => a.Usuario);
            return View(auditorias.ToList());
        }

        // GET: Mobile/Auditoria/Details/5
        public ActionResult Details(int idSucursal)
        {
            Sucursal sucursal = db.Sucursals.Find(idSucursal);
            Usuario usuario = db.Usuarios.FirstOrDefault(e => e.UserName == User.Identity.Name);
            Auditoria a = new Auditoria
            {
                Usuario = usuario,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now,
                Sucursal = sucursal
            };
            db.Auditorias.Add(a);
            db.SaveChanges();
            return RedirectToAction("NewAuditoria", a);
        }

        // GET: Mobile/Auditoria/Create
        public ActionResult Create(Sucursal sucursal)
        {
            Usuario usuario = db.Usuarios.Find(User.Identity.Name);
            Auditoria a = new Auditoria { Usuario = usuario, FechaFin = DateTime.Now, Sucursal = sucursal };
            db.Auditorias.Add(a);
            db.SaveChanges();
            return View();
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
            //Controlar que no tenga pendientes autitorias abiertas.
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
