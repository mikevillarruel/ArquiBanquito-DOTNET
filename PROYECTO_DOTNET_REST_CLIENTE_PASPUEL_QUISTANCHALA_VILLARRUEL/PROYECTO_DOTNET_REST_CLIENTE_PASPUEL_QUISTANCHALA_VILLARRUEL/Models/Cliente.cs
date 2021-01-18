using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_DOTNET_REST_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Models
{
    public class Cliente
    {
        public int id_cliente { get; set; }
        public string nombres_cliente { get; set; }
        public string apellidos_cliente { get; set; }
        public string cedula_cliente { get; set; }
        public string direccion_cliente { get; set; }
        public string telefono_cliente { get; set; }
        public string correo_cliente { get; set; }

    }
}