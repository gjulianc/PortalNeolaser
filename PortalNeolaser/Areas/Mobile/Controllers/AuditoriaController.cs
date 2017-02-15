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
            //Si existe alguna auditoria abierta de esta sucursal, se presenta y bien sigue auditando o la cierra y luego abre otra.
            Auditoria a = new Auditoria();
            a = ExiteAuditoriaAbiertaBySucursal(idSucursal);
            if (a == null)
            {
                Auditoria tmp = new Auditoria();
                tmp.Usuario = usuario;
                tmp.FechaInicio = DateTime.Now;
                tmp.FechaFin = DateTime.Now;
                tmp.Sucursal = sucursal;
                tmp.Estado = false;
                a = tmp;
                db.Auditorias.Add(a);
                db.SaveChanges();
                MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega a la auditoria Abierta {2}.", DateTime.Now, User.Identity.Name, a.Id));
                return RedirectToAction("NewAuditoria", a);
            }
            else
            {
                MvcApplication.Log.WriteLog(String.Format("{0};Navegación;{1};Navega a Elementos de Nueva Auditoria {2}.", DateTime.Now, User.Identity.Name,a.Id));
                return RedirectToAction("OpenAuditoria", a);
            }
            
        }

        private Auditoria ExiteAuditoriaAbiertaBySucursal(int idSucursal)
        {
            return db.Auditorias.FirstOrDefault(e => e.Estado == false && e.FkSucursal == idSucursal);
        }

        // GET: Mobile/Auditoria/Create
        public ActionResult Create(Sucursal sucursal)
        {
            Usuario usuario = db.Usuarios.Find(User.Identity.Name);
            Auditoria a = new Auditoria { Usuario = usuario, FechaFin = DateTime.Now, Sucursal = sucursal };
            db.Auditorias.Add(a);
            db.SaveChanges();
            MvcApplication.Log.WriteLog(String.Format("{0};Acceso Base Datos;{1};Crea nueva Auditoría Id {2}.", DateTime.Now, User.Identity.Name, a.Id));
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

            return RedirectToAction("Index", "ElementoAuditado", new { IdAuditoria = auditoria.Id, esNueva = true });
        }

        //Continuacion de Auditoria
        public ActionResult OpenAuditoria(Auditoria auditoria)
        {
            
            return RedirectToAction("Index", "ElementoAuditado", new { IdAuditoria = auditoria.Id, esNueva = false });
        }

        public ActionResult FinalizarAuditoria(int idAuditoria)
        {
            //actualizar la auditoria a estado = true

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
