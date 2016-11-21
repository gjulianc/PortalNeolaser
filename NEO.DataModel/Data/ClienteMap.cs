using PortalNeolaser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEO.DataModel.Data
{
    public class ClienteMap : EntityTypeConfiguration<Cliente>
    {
        protected EFDbContext ctx = new EFDbContext();

        public ClienteMap()
        {
            //Key
            HasKey(t => t.Cliente_Id);

            //Property
            Property(t => t.Cliente_Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Cif);
            Property(t => t.Nombre);
            Property(t => t.Telefono);
            Property(t => t.Email);
            Property(t => t.Logo);
            Property(t => t.Direccion);
            Property(t => t.Poblacion);
            Property(t => t.Provincia);
            Property(t => t.CodPostal);


            //table
            ToTable("Clientes");
        }
    }
}
