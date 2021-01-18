using PROYECTO_DOTNET_REST_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Controllers;
using PROYECTO_DOTNET_REST_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace PROYECTO_DOTNET_REST_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL.Filters
{
    public class VerificaSession : ActionFilterAttribute
    {
        private Usuario oUsuario = null;
        public bool Disable { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                oUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
                if (Disable == true)
                {
                    return;
                }
                if (oUsuario == null)
                {

                    if (filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                    }
                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }

        }
    }
}