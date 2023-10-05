using OrchardCore.DisplayManagement.Entities;
using OrchardCore.Settings;

namespace OrchardCore.TawkTo.Settings
{
    public class TawkToSettings : SectionDisplayDriver<ISite, TawkToSettings>
    {
        public string PropertyId { get; set; }
        public string WidgetId { get; set; }
    }
}
