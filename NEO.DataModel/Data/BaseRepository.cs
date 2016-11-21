using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NEO.DataModel.Data
{
    public class BaseRepository<T> where T : class
    {
        protected DbContext ctx = new EFDbContext();
        protected DbSet<T> DbSet;

        public BaseRepository()
        {
            DbSet = ctx.Set<T>();
        }

        public void Insertar(T entidad)
        {
            DbSet.Add(entidad);
        }
        public void Eliminar(T entidad)
        {
            DbSet.Remove(entidad);
        }

        public IQueryable<T> Fitrar(Expression<Func<T, bool>> expresion)
        {
            return DbSet.Where(expresion);
        }

        public T ObtenerPorId(int id)
        {
            return DbSet.Find(id);
        }
        public IQueryable<T> ObtenerTodos()
        {
            return DbSet;
        }

        public void GuardarCambios()
        {
            ctx.SaveChanges();
        }


    }
}
