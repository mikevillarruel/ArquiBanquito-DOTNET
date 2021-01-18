using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_DOTNET_REST_PASPUEL_QUISTANCHALA_VILLARRUEL.Modelo
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public int id_cliente { get; set; }
        public string cedula_cliente { get; set; }
        public string nombre_usuario { get; set; }
        public string password_usuario { get; set; }
        public int cambio_usuario { get; set; }
    }
}