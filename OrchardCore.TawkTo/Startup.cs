using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.TawkTo.Drivers;
using OrchardCore.TawkTo.Recipes;
using OrchardCore.TawkTo.Services;
using OrchardCore.TawkTo.Settings;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Recipes;
using OrchardCore.ResourceManagement;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using System;

namespace OrchardCore.TawkTo
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<TawkToSettings>, TawkToSettingsConfiguration>();
            services.AddScoped<IPermissionProvider, Permissions.TawkTo>();
            services.AddRecipeExecutionStep<TawkToSettingsStep>();
            services.AddScoped<IDisplayDriver<ISite>, TawkToSettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, TawkToAdminMenu>();
            services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();

            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.Add(typeof(TawkToFilter));
            });
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "Home",
                areaName: "OrchardCore.TawkTo",
                pattern: "Home/Index",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
