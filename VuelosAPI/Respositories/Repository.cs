using Microsoft.EntityFrameworkCore;
using VuelosAPI.Models;

namespace VuelosAPI.Respositories
{
    public class Repository<T> where T : class
    {
        private readonly Sistem21AerolineaDbContext context;
        public Repository(Sistem21AerolineaDbContext context) 
        {
            this.context = context;
        } 
        public DbSet<T> Get()
        {
            return context.Set<T>();
        }
        public T? Get(object id)
        {
            return context.Find<T>(id);
        }
        public void Insert(T entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }
        public void Update(T entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
        public void Delete(T entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }
    }
}
