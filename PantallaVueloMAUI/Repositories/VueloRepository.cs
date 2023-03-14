using MetricKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PantallaVueloMAUI.Models;
using SQLite;
using PantallaVueloMAUI.Helpers;
using Windows.Gaming.Input;

namespace PantallaVueloMAUI.Repositories
{
    public class VueloRepository<T> where T: new ()
    {
        SQLiteConnection context;
        public VueloRepository()
        {
            context = new SQLiteConnection(DatabaseHelper.RutaBD);
            context.CreateTable<Vuelo>();
            context.CreateTable<VueloBuffer>();
        }
        public TableQuery<T> GetAll()
        {
            return context.Table<T>();
        }
        public T Get(int Id)
        {
            return context.Get<T>(Id);  
        }
        public void Insert(T t)
        {
            context.Insert(t);
        }
        public void Update(T t)
        {
            context.Update(t);
        }
        public void Delete(int Id)
        {
            context.Delete<Vuelo>(Id);
        }

    }
}
