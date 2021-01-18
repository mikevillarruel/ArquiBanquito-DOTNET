using PROYECTO_DOTNET_REST_PASPUEL_QUISTANCHALA_VILLARRUEL.Modelo;
using PROYECTO_DOTNET_REST_PASPUEL_QUISTANCHALA_VILLARRUEL.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PROYECTO_DOTNET_REST_PASPUEL_QUISTANCHALA_VILLARRUEL.Controllers
{
    public class CoreBancarioController : ApiController
    {
        [HttpGet]
        public Cliente verificarCliente(String cedula, String cuenta)
        {
            CoreBancarioService service = new CoreBancarioService();
            return service.verificarCliente(cedula, cuenta);
        }

        [HttpGet]
        public Object inicioSesion(String usuarioNombre, String contrasena)
        {
            CoreBancarioService service = new CoreBancarioService();
            var respuesta = new
            {
                resultado = service.inicioSesion(usuarioNombre, contrasena)
            };
            return respuesta;
        }

        [HttpGet]
        public List<Cuenta> posicionConsolidada(String cedula)
        {
            CoreBancarioService service = new CoreBancarioService();
            return service.posicionConsolidada(cedula);
        }

        [HttpGet]
        public List<Movimiento> detalleMovimientos(String cuenta)
        {
            CoreBancarioService service = new CoreBancarioService();
            return service.detalleMovimientos(cuenta);
        }

        [HttpGet]
        public Object transferencias(String cuentaOrigen, Double importe, String cuentaDestino)
        {
            CoreBancarioService service = new CoreBancarioService();
            var respuesta = new
            {
                resultado = service.transferencias(cuentaOrigen, importe, cuentaDestino)
            };
            return respuesta;
        }

        [HttpGet]
        public Object registrarCuentaBancaria(int idCliente, String tipoCuenta, double saldoInicial)
        {
            CoreBancarioService service = new CoreBancarioService();
            var respuesta = new
            {
                resultado = service.registrarCuentaBancaria(idCliente, tipoCuenta, saldoInicial)
            };
            return respuesta;
        }

        [HttpGet]
        public Usuario obtenerUsuario(string nombreUsuario)
        {
            CoreBancarioService service = new CoreBancarioService();
            return service.obtenerUsuario(nombreUsuario);
        }

        [HttpGet]
        public Object actualizarContrasena(String nombreUsuario, String contrasenia)
        {
            CoreBancarioService service = new CoreBancarioService();
            var respuesta = new
            {
                resultado = service.actualizarContrasena(nombreUsuario, contrasenia)
            };
            return respuesta;
        }
    }
}
