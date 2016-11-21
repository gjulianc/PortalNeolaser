using NEO.DataModel.Contract;
using PortalNeolaser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace NEO.DataModel.Data
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>, IUsuarioRepository
    {
        protected EFDbContext ctx = new EFDbContext();

        public UsuarioMap()
        {
            //Key
            HasKey(t => t.Usuario_Id);

            //Property
            Property(t => t.Usuario_Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Nombre);
            Property(t => t.User);
            Property(t => t.Password);

            //table
            ToTable("Usuarios");
        }

        public void DeleteUser(Usuario usuario)
        {
            ctx.Set<Usuario>().Remove(usuario);
            GuardarDatos();
        }

        public List<Usuario> GetAllUsers()
        {
            return ctx.Set<Usuario>().ToList();
        }

        public Usuario GetUser(int? id)
        {
            return ctx.Set<Usuario>().Find(id);
        }

        public void GuardarDatos()
        {
            ctx.SaveChanges();
        }

        public void InsertUser(Usuario usuario)
        {
            ctx.Set<Usuario>().Add(usuario);
            this.GuardarDatos();
        }

        public Usuario Login(string usuario, string password)
        {
            var usr = ctx.Set<Usuario>().FirstOrDefault(u => u.User == usuario && u.Password == password);
            return usr;
            
        }
    }
}
