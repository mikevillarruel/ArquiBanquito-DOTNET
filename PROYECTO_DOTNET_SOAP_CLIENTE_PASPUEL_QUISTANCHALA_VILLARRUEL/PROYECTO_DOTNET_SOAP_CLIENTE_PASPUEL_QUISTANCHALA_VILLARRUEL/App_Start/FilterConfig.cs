using System.Web;
using System.Web.Mvc;

namespace PROYECTO_DOTNET_SOAP_CLIENTE_PASPUEL_QUISTANCHALA_VILLARRUEL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filters.VerificaSession());
        }
    }
}
