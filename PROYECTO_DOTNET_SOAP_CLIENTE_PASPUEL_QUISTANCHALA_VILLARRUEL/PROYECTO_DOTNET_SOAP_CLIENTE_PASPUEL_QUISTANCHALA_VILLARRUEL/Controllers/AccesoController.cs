using PROYECTO_DOTNET_SOAP_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Filters;
using PROYECTO_DOTNET_SOAP_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROYECTO_DOTNET_SOAP_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string nombreUsuario, string contrasenia)
        {
            contrasenia = Encriptar(contrasenia);
            CoreBancarioService service = new CoreBancarioService();
            if (service.inicioSesion(nombreUsuario, contrasenia))
            {
                Session["Usuario"] = service.obtenerUsuario(nombreUsuario);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Usuario o Contraseña Incorrecta";
                return View();
            }
        }

        public String Encriptar(String contrasenia)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(contrasenia);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Login", "Acceso");
        }

        [HttpGet]
        [VerificaSession(Disable = true)]
        public ActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        [VerificaSession(Disable = true)]
        public ActionResult RegistrarUsuario(String cedula, String nroCuenta)
        {
            CoreBancarioService service = new CoreBancarioService();
            if (service.verificarCliente(cedula, nroCuenta).id_cliente!=0)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                ViewBag.Message = "Datos incorrectos o el usuario ya ha sido creado anteriormente";
                return View();
            }
        }


    }
}