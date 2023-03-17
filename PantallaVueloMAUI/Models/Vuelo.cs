using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaVueloMAUI.Models
{
   public class Vuelo
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Destino { get; set; } = null!;

        public string Numerovuelo { get; set; } = null!;

        public int Puerta { get; set; }

        public string Estado { get; set; } = "";

        public DateTime Fecha { get; set; } = DateTime.Now;

        public DateTime UltimaEdicionFecha { get; set; }
    }
}
