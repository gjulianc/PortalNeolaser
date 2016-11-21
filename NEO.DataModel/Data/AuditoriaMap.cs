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
    public class AuditoriaMap: EntityTypeConfiguration<Auditoria>
    {
        protected EFDbContext ctx = new EFDbContext();

        public AuditoriaMap()
        {
            //Key
            HasKey(t => t.Auditoria_Id);

            //Property
            Property(t => t.Auditoria_Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FechaInicio);
            Property(t => t.FechaFin);


            //table
            ToTable("Auditorias");
        }
    }
}
