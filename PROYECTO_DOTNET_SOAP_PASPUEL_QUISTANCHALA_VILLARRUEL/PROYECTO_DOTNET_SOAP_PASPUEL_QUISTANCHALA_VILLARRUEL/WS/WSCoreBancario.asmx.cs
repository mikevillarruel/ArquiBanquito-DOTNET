using PROYECTO_DOTNET_SOAP_PASPUEL_QUISTANCHALA_VILLARRUEL.Modelo;
using PROYECTO_DOTNET_SOAP_PASPUEL_QUISTANCHALA_VILLARRUEL.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PROYECTO_DOTNET_SOAP_PASPUEL_QUISTANCHALA_VILLARRUEL.WS
{
    /// <summary>
    /// Descripción breve de WSCoreBancario
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSCoreBancario : System.Web.Services.WebService
    {

        [WebMethod]
        public Cliente verificarCliente(String cedula, String cuenta)
        {
            CoreBancarioService service = new CoreBancarioService();
            return service.verificarCliente(cedula, cuenta);
        }

        [WebMethod]
        public Boolean inicioSesion(String usuarioNombre, String contrasena)
        {
            CoreBancarioService service = new CoreBancarioService();
            return service.inicioSesion(usuarioNombre, contrasena);
        }

        [WebMethod]
        public List<Cuenta> posicionConsolidada(String cedula)
        {
            CoreBancarioService service = new CoreBancarioService();
            return service.posicionConsolidada(cedula);
        }

        [WebMethod]
        public List<Movimiento> detalleMovimientos(String cuenta)
        {
            CoreBancarioService service = new CoreBancarioService();
            return service.detalleMovimientos(cuenta);
        }

        [WebMethod]
        public Boolean transferencias(String cuentaOrigen, Double importe, String cuentaDestino)
        {
            CoreBancarioService service = new CoreBancarioService();
            return service.transferencias(cuentaOrigen, importe, cuentaDestino);
        }

        [WebMethod]
        public Boolean registrarCuentaBancaria(int idCliente, String tipoCuenta, double saldoInicial)
        {
            CoreBancarioService service = new CoreBancarioService();
            return service.registrarCuentaBancaria(idCliente, tipoCuenta, saldoInicial);
        }

        [WebMethod]
        public Usuario obtenerUsuario(string nombreUsuario)
        {
            CoreBancarioService service = new CoreBancarioService();
            return service.obtenerUsuario(nombreUsuario);
        }

        [WebMethod]
        public Boolean actualizarContrasena(String nombreUsuario, String contrasenia)
        {
            CoreBancarioService service = new CoreBancarioService();
            return service.actualizarContrasena(nombreUsuario, contrasenia);
        }
    }
}
