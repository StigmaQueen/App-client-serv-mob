using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RifasMauiApp.Models
{
    internal class BoletoDTO
    {
        public int Id { get; set; }

        public uint NumeroBoleto { get; set; }

        public string NombrePersona { get; set; } = null!;

        public string NumeroTelefono { get; set; } = null!;

        public ulong Pagado { get; set; }

        public ulong Eliminado { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
