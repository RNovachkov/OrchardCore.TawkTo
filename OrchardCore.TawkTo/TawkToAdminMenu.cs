using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace OrchardCore.TawkTo
{
    public class TawkToAdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;

        public TawkToAdminMenu(IStringLocalizer<TawkToAdminMenu> localizer)
        {
            S = localizer;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                builder.Add(S["Configuration"], configuration => configuration
                    .Add(S["Settings"], settings => settings
                        .Add(S["TawkTo"], S["TawkTo"].PrefixPosition(), settings => settings
                            .Permission(Permissions.ManageTawkTo)
                            .AddClass("tawkto").Id("tawkto")
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = TawkToConstants.TawkToSettings })
                            .LocalNav())
                        )
                );
            }
            return Task.CompletedTask;
        }
    }
}
