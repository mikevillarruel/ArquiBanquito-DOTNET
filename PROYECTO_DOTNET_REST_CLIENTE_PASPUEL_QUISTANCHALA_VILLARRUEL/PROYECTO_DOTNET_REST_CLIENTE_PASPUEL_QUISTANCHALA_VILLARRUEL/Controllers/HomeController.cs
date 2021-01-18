using PROYECTO_DOTNET_REST_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Servicio;
using PROYECTO_DOTNET_REST_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PROYECTO_DOTNET_REST_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> PosicionConsolidada()
        {
            CoreBancarioService service = new CoreBancarioService();
            Usuario us = (Usuario)HttpContext.Session["Usuario"];
            List<Cuenta> model = new List<Cuenta>();
            if (us != null)
            {
                model = await service.posicionConsolidada(us.cedula_cliente);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> DetalleMovimientos(String cuenta)
        {
            ViewBag.Cuenta = cuenta;
            CoreBancarioService service = new CoreBancarioService();
            var model = await service.detalleMovimientos(cuenta);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Transferencias()
        {
            return View(await cuentas());
        }

        [HttpPost]
        public async Task<ActionResult> Transferencias(String cuentaOrigen, String importe, String cuentaDestino)
        {
            CoreBancarioService service = new CoreBancarioService();
            ViewBag.Message = await service.transferencias(cuentaOrigen, Double.Parse(importe, System.Globalization.CultureInfo.InvariantCulture), cuentaDestino);
            return View(await cuentas());
        }

        public async Task<List<String>> cuentas()
        {
            CoreBancarioService service = new CoreBancarioService();
            Usuario us = (Usuario)HttpContext.Session["Usuario"];
            List<Cuenta> cuentas = new List<Cuenta>();
            if (us != null)
            {
                cuentas = await service.posicionConsolidada(us.cedula_cliente);
            }
            List<String> listaNroCuentas = new List<String>();
            foreach (Cuenta item in cuentas)
            {
                listaNroCuentas.Add(item.nro_cuenta);
            }
            return listaNroCuentas;
        }

        [HttpGet]
        public ActionResult CrearCuenta()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CrearCuenta(String tipoCuenta, String montoInicial)
        {
            CoreBancarioService service = new CoreBancarioService();
            Usuario us = (Usuario)HttpContext.Session["Usuario"];
            if (us != null)
            {
                ViewBag.Message = await service.registrarCuentaBancaria(us.id_cliente, tipoCuenta, Double.Parse(montoInicial, System.Globalization.CultureInfo.InvariantCulture));
            }
            return View();
        }

    }
}