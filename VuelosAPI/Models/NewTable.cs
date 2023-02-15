using System;
using System.Collections.Generic;

namespace VuelosAPI.Models;

public partial class NewTable
{
    public int Id { get; set; }

    public string Destino { get; set; } = null!;

    public string NumeroVuelo { get; set; } = null!;

    public int Puerta { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime Fecha { get; set; }
}
