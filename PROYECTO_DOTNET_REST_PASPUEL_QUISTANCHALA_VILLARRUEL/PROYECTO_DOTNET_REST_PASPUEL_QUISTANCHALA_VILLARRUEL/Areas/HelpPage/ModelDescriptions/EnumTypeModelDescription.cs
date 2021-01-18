using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PROYECTO_DOTNET_REST_PASPUEL_QUISTANCHALA_VILLARRUEL.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}