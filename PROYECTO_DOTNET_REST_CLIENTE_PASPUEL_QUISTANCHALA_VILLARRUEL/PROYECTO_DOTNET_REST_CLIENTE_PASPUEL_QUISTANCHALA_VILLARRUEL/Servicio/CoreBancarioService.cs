using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PROYECTO_DOTNET_REST_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PROYECTO_DOTNET_REST_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Servicio
{
    public class CoreBancarioService
    {
        string localhost = "https://localhost:44349/api/CoreBancario/";
        public async Task<Cliente> verificarCliente(String cedula, String cuenta)
        {
            string url = localhost + "verificarCliente?cedula=" + cedula + "&cuenta=" + cuenta;
            string request = await new HttpClient().GetStringAsync(url);
            return JsonConvert.DeserializeObject<Cliente>(request);
        }

        public async Task<Boolean> inicioSesion(String usuarioNombre, String contrasena)
        {
            string url = localhost + "inicioSesion?usuarioNombre=" + usuarioNombre + "&contrasena=" + contrasena;
            string request = await new HttpClient().GetStringAsync(url);
            JObject obj = JsonConvert.DeserializeObject<JObject>(request);
            return (Boolean)obj.GetValue("resultado");
        }

        public async Task<List<Cuenta>> posicionConsolidada(String cedula)
        {
            string url = localhost + "posicionConsolidada?cedula=" + cedula;
            string request = await new HttpClient().GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Cuenta>>(request);
        }

        public async Task<List<Movimiento>> detalleMovimientos(String cuenta)
        {
            string url = localhost + "detalleMovimientos?cuenta=" + cuenta;
            string request = await new HttpClient().GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Movimiento>>(request);
        }

        public async Task<Boolean> transferencias(String cuentaOrigen, Double importe, String cuentaDestino)
        {
            string url = localhost + "transferencias?cuentaOrigen=" + cuentaOrigen + "&importe=" + (importe + "").Replace(",", ".") + "&cuentaDestino=" + cuentaDestino;
            string request = await new HttpClient().GetStringAsync(url);
            JObject obj = (JObject)JsonConvert.DeserializeObject(request);
            return (Boolean)obj.GetValue("resultado");
        }

        public async Task<Boolean> registrarCuentaBancaria(int idCliente, String tipoCuenta, double saldoInicial)
        {
            string url = localhost + "registrarCuentaBancaria?idCliente=" + idCliente + "&tipoCuenta=" + tipoCuenta + "&saldoInicial=" + (saldoInicial + "").Replace(",", ".");
            string request = await new HttpClient().GetStringAsync(url);
            JObject obj = (JObject)JsonConvert.DeserializeObject(request);
            return (Boolean)obj.GetValue("resultado");
        }

        public async Task<Usuario> obtenerUsuario(string nombreUsuario)
        {
            string url = localhost + "obtenerUsuario?nombreUsuario=" + nombreUsuario;
            string request = await new HttpClient().GetStringAsync(url);
            return JsonConvert.DeserializeObject<Usuario>(request);
        }

        public async Task<Boolean> actualizarContrasena(String nombreUsuario, String contrasenia)
        {
            string url = localhost + "actualizarContrasena?nombreUsuario=" + nombreUsuario + "&contrasenia=" + contrasenia;
            string request = await new HttpClient().GetStringAsync(url);
            JObject obj = (JObject)JsonConvert.DeserializeObject(request);
            return (Boolean)obj.GetValue("resultado");
        }
    }
}