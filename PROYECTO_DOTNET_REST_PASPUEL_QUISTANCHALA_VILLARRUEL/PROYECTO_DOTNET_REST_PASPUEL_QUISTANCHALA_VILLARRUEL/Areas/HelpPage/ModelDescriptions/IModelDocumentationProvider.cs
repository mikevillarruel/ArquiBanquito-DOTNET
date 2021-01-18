using System;
using System.Reflection;

namespace PROYECTO_DOTNET_REST_PASPUEL_QUISTANCHALA_VILLARRUEL.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}