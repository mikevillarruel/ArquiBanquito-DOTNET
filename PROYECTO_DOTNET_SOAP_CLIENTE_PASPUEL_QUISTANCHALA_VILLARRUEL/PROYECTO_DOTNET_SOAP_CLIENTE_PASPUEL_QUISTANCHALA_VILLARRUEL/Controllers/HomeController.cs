using PROYECTO_DOTNET_SOAP_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Service;
using PROYECTO_DOTNET_SOAP_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.WSCoreBancario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROYECTO_DOTNET_SOAP_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PosicionConsolidada()
        {
            CoreBancarioService service = new CoreBancarioService();
            Usuario us = (Usuario)HttpContext.Session["Usuario"];
            List<Cuenta> model = new List<Cuenta>();
            if (us != null)
            {
                model = service.posicionConsolidada(us.cedula_cliente);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DetalleMovimientos(String cuenta)
        {
            ViewBag.Cuenta = cuenta;
            CoreBancarioService service = new CoreBancarioService();
            var model = service.detalleMovimientos(cuenta);
            return View(model);
        }

        [HttpGet]
        public ActionResult Transferencias()
        {
            return View(cuentas());
        }

        [HttpPost]
        public ActionResult Transferencias(String cuentaOrigen, String importe, String cuentaDestino)
        {
            CoreBancarioService service = new CoreBancarioService();
            ViewBag.Message = service.transferencias(cuentaOrigen, Double.Parse(importe, System.Globalization.CultureInfo.InvariantCulture), cuentaDestino);
            return View(cuentas());
        }

        public List<String> cuentas()
        {
            CoreBancarioService service = new CoreBancarioService();
            Usuario us = (Usuario)HttpContext.Session["Usuario"];
            List<Cuenta> cuentas = new List<Cuenta>();
            if (us != null)
            {
                cuentas = service.posicionConsolidada(us.cedula_cliente);
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
        public ActionResult CrearCuenta(String tipoCuenta, String montoInicial)
        {
            CoreBancarioService service = new CoreBancarioService();
            Usuario us = (Usuario)HttpContext.Session["Usuario"];

            if (us != null)
            {
                ViewBag.Message = service.registrarCuentaBancaria(us.id_cliente, tipoCuenta, Double.Parse(montoInicial, System.Globalization.CultureInfo.InvariantCulture));
            }

            return View();
        }

    }
}