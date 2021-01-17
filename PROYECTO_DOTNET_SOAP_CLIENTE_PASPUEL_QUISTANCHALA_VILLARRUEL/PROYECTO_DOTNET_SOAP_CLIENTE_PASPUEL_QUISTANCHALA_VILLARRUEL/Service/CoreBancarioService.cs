using PROYECTO_DOTNET_SOAP_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.WSCoreBancario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_DOTNET_SOAP_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Service
{
    public class CoreBancarioService
    {
        WSCoreBancario.WSCoreBancarioSoapClient service = new WSCoreBancario.WSCoreBancarioSoapClient();
        public Cliente verificarCliente(String cedula, String cuenta)
        {
            return service.verificarCliente(cedula, cuenta);
        }

        public Boolean inicioSesion(String usuarioNombre, String contrasena)
        {
            return service.inicioSesion(usuarioNombre, contrasena);
        }

        public List<Cuenta> posicionConsolidada(String cedula)
        {
            return service.posicionConsolidada(cedula);
        }

        public List<Movimiento> detalleMovimientos(String cuenta)
        {
            return service.detalleMovimientos(cuenta);
        }

        public Boolean transferencias(String cuentaOrigen, Double importe, String cuentaDestino)
        {
            return service.transferencias(cuentaOrigen, importe, cuentaDestino);
        }

        public Boolean registrarCuentaBancaria(int idCliente, String tipoCuenta, double saldoInicial)
        {
            return service.registrarCuentaBancaria(idCliente, tipoCuenta, saldoInicial);
        }

        public Usuario obtenerUsuario(string nombreUsuario)
        {
            return service.obtenerUsuario(nombreUsuario);
        }

        public Boolean actualizarContrasena(String nombreUsuario, String contrasenia)
        {
            return service.actualizarContrasena(nombreUsuario, contrasenia);
        }
    }
}