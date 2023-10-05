using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OrchardCore.TawkTo.Settings;
using OrchardCore.Environment.Shell.Configuration;

namespace OrchardCore.TawkTo.Services
{
    public class TawkToSettingsConfiguration : IConfigureOptions<TawkToSettings>
    {
        private readonly IShellConfiguration _shellConfiguration;
        private readonly IConfiguration _generalConfiguration;

        public TawkToSettingsConfiguration(IShellConfiguration shellConfiguration, IConfiguration generalConfiguration)
        {
            _shellConfiguration = shellConfiguration;
            _generalConfiguration = generalConfiguration;
        }


        public void Configure(TawkToSettings options)
        {
            var section = _shellConfiguration.GetSection("TawkTo");
            if (!section.Exists())
            {
                section = _generalConfiguration.GetSection("TawkTo");
            }
            options.PropertyId = section.GetSection("PropertyId").Get<string>();
            options.WidgetId = section.GetSection("WidgetId").Get<string>();
        }
    }
}
