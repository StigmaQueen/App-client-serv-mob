using Microsoft.EntityFrameworkCore;
using ToDoListU1_E1.Models;

namespace ToDoListU1_E1.Repositories
{

    public class Repository<T> where T : class
    {
        private readonly Sistem21TodolistContext context;

        public Repository(Sistem21TodolistContext context)
        {
            this.context = context;
        }

        public DbSet<T> GetAll()
        {
            return context.Set<T>();
        }

        public T? GetByID(object id)
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
