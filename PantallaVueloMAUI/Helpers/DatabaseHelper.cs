using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaVueloMAUI.Helpers
{
    public class DatabaseHelper
    {
        public static string NombreBD => "vuelos.db3";
        public static string RutaBD => Path.Combine(FileSystem.AppDataDirectory,NombreBD);
    }
}
