using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_DOTNET_REST_PASPUEL_QUISTANCHALA_VILLARRUEL.Modelo
{
    public class Cuenta
    {
        public int id_cuenta { get; set; }
        public int id_cliente { get; set; }
        public string nro_cuenta { get; set; }
        public string tipo_cuenta { get; set; }
        public decimal saldo_cuenta { get; set; }

    }
}