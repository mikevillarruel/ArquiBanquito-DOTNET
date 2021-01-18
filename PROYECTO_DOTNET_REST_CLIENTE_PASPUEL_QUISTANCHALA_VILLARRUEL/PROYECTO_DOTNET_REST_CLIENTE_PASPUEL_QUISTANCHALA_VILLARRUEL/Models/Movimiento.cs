using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_DOTNET_REST_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Models
{
    public class Movimiento
    {
        public int id_movimiento { get; set; }
        public int id_cuenta { get; set; }
        public DateTime fecha_movimiento { get; set; }
        public string tipo_movimiento { get; set; }
        public decimal importe_movimiento { get; set; }
        public string destino_movimiento { get; set; }
        public decimal saldo_movimiento { get; set; }

    }
}