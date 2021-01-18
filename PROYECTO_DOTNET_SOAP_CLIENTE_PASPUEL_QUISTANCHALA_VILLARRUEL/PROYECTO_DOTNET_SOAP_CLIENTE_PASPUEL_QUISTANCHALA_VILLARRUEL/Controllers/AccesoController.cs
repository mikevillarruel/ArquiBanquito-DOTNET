using PROYECTO_DOTNET_SOAP_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Filters;
using PROYECTO_DOTNET_SOAP_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Service;
using PROYECTO_DOTNET_SOAP_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.WSCoreBancario;
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
                Usuario us = service.obtenerUsuario(nombreUsuario);
                Session["Usuario"] = us;
                if (us.cambio_usuario == 0)
                {
                    return RedirectToAction("ActualizarContrasenia", "Acceso");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
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

        [HttpGet]
        public ActionResult ActualizarContrasenia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ActualizarContrasenia(String nuevaContrasenia, String nuevaContraseniaR)
        {
            CoreBancarioService service = new CoreBancarioService();
            Usuario us = (Usuario)HttpContext.Session["Usuario"];
            if (nuevaContrasenia.Equals(nuevaContraseniaR))
            {
                if (us != null)
                {
                    if (service.actualizarContrasenia(us.nombre_usuario, nuevaContrasenia))
                    {
                        Session["Usuario"] = service.obtenerUsuario(us.nombre_usuario);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return CerrarSesion();
                    }
                }
                else
                {
                    return CerrarSesion();
                }
            }
            else {
                ViewBag.Message = "Las contraseñas ingresadas no son iguales";
                return View();
            }

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
            if (ValidarCedula(cedula)){
                CoreBancarioService service = new CoreBancarioService();
                if (service.verificarCliente(cedula, nroCuenta).id_cliente != 0)
                {
                    return RedirectToAction("Login", "Acceso");
                }
                else
                {
                    ViewBag.Message = "Datos incorrectos o el usuario ya ha sido creado anteriormente";
                    return View();
                }
            }
            else {
                ViewBag.Message = "Cédula Inválida";
                return View();
            }
        }

        public Boolean ValidarCedula(String cedula)
        {
            try
            {
                int suma = 0;
                if (cedula.Length != 10)
                {
                    return false;
                }
                else
                {
                    int[] a = new int[cedula.Length / 2];
                    int[] b = new int[(cedula.Length / 2)];
                    int c = 0;
                    int d = 1;
                    for (int i = 0; i < cedula.Length / 2; i++)
                    {
                        a[i] = int.Parse(cedula.ElementAt(c)+"");
                        c = c + 2;
                        if (i < (cedula.Length / 2) - 1)
                        {
                            b[i] = int.Parse(cedula.ElementAt(d)+"");
                            d = d + 2;
                        }
                    }

                    for (int i = 0; i < a.Length; i++)
                    {
                        a[i] = a[i] * 2;
                        if (a[i] > 9)
                        {
                            a[i] = a[i] - 9;
                        }
                        suma = suma + a[i] + b[i];
                    }
                    int aux = suma / 10;
                    int dec = (aux + 1) * 10;
                    if ((dec - suma) == int.Parse(cedula.ElementAt(cedula.Length - 1)+""))
                    {
                        return true;
                    }
                    else if (suma % 10 == 0 && cedula.ElementAt(cedula.Length - 1) == '0')
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception e) {
                return false;
            }
        }

    }
}