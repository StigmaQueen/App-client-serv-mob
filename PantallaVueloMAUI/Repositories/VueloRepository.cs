using MetricKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PantallaVueloMAUI.Models;
using SQLite;
using PantallaVueloMAUI.Helpers;

namespace PantallaVueloMAUI.Repositories
{
    public class VueloRepository
    {
        SQLiteConnection context;
        public VueloRepository()
        {
            context = new SQLiteConnection(DatabaseHelper.RutaBD);
            context.CreateTable<Vuelo>();
        }
        public List<Vuelo> GetAll()
        {
            return new List<Vuelo>();
        }
        public List<Vuelo> Get(int Id)
        {
            return new List<Vuelo>(Id);
        }
        public void Insert(Vuelo v)
        {
            context.Insert(v);
        }
        public void Update(Vuelo v)
        {
            context.Update(v);
        }
        public void Delete(int Id)
        {
            context.Delete<Vuelo>(Id);
        }

    }
}
