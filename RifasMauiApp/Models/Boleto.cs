using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RifasMauiApp.Models
{
    internal class Boleto
    {
        [PrimaryKey]
        public int Id { get; set; }

        public uint NumeroBoleto { get; set; }

        public string NombrePersona { get; set; } = null!;

        public string NumeroTelefono { get; set; } = null!;

        public bool Pagado { get; set; }
    }
}
