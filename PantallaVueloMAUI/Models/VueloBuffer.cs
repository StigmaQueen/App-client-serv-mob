﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaVueloMAUI.Models
{
    public enum State { Agregado, Modificado, Eliminado}
    public class VueloBuffer
    {
        //Para controlar el buffer
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; } 
        public State Status { get; set; }


        //De la entidad
        public string Destino { get; set; } = null!;

        public string Numerovuelo { get; set; } = null!;

        public int Puerta { get; set; }

        public string Estado { get; set; } = null!;

        public DateTime Fecha { get; set; }

    }
}
