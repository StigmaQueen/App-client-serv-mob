using System;
using System.Collections.Generic;

namespace EjercisioRifas.Models;

public partial class Boletos
{
    public int Id { get; set; }

    public uint NumeroBoleto { get; set; }

    public string NombrePersona { get; set; } = null!;

    public string NumeroTelefono { get; set; } = null!;

    public ulong Pagado { get; set; }

    public ulong Eliminado { get; set; }

    public DateTime FechaModificacion { get; set; }
}
