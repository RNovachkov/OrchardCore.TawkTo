using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace OrchardCore.TawkTo
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static ResourceManifest _manifest;

        static ResourceManagementOptionsConfiguration()
        {
            _manifest = new ResourceManifest();

            _manifest
                .DefineScript("tawk-to")
                .SetAttribute("data-cfasync", "false")
                .SetUrl("~/OrchardCore.TawkTo/scripts/tawk-to.min.js", "~/OrchardCore.TawkTo/scripts/tawk-to.js")
                .SetVersion("0.0.1");
        }

        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(_manifest);
        }
    }
}
