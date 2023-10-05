using OrchardCore.DisplayManagement.Entities;
using OrchardCore.Settings;

namespace OrchardCore.TawkTo.ViewModels
{
    public class TawkToSettingsViewModel : SectionDisplayDriver<ISite, TawkToSettingsViewModel>
    {
        public string PropertyId { get; set; }
        public string WidgetId { get; set; }
    }
}
