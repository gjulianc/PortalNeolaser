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
    public class SucursalMap : EntityTypeConfiguration<Sucursal>
    {
        protected EFDbContext ctx = new EFDbContext();

        public SucursalMap()
        {
            //Key
            HasKey(t => t.Sucursal_Id);

            //Property
            Property(t => t.Sucursal_Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CodigoSAP);
            Property(t => t.Descripcion);
            Property(t => t.Direccion);
            Property(t => t.Poblacion);
            Property(t => t.Provincia);
            Property(t => t.CodPostal);

            //table
            ToTable("Sucursales");
        }
    }
}
