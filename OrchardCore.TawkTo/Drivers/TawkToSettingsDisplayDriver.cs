using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OrchardCore.TawkTo.Settings;
using OrchardCore.TawkTo.ViewModels;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using System.Threading.Tasks;

namespace OrchardCore.TawkTo.Drivers
{
    public class TawkToSettingsDisplayDriver : SectionDisplayDriver<ISite, TawkToSettings>
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IConfiguration config;

        public TawkToSettingsDisplayDriver(
            IAuthorizationService authorizationService,
            IHttpContextAccessor contextAccessor,
            IConfiguration config
            )
        {
            this.authorizationService = authorizationService;
            this.contextAccessor = contextAccessor;
            this.config = config;
        }

        public override async Task<IDisplayResult> EditAsync(TawkToSettings settings, BuildEditorContext context)
        {
            var user = this.contextAccessor.HttpContext?.User;
            if (!await this.authorizationService.AuthorizeAsync(user, Permissions.ManageTawkTo))
            {
                return null;
            }
            return Initialize<TawkToSettingsViewModel>("TawkToSettings_Edit", model =>
            {
                model.PropertyId = settings.PropertyId;
                model.WidgetId = settings.WidgetId;
            }).Location("Content:5").OnGroup(TawkToConstants.TawkToSettings);
        }

        public override async Task<IDisplayResult> UpdateAsync(TawkToSettings settings, BuildEditorContext context)
        {
            if (context.GroupId == TawkToConstants.TawkToSettings)
            {
                var user = this.contextAccessor.HttpContext?.User;
                if (user == null || !await this.authorizationService.AuthorizeAsync(user, Permissions.ManageTawkTo))
                {
                    return null;
                }

                var model = new TawkToSettingsViewModel();
                await context.Updater.TryUpdateModelAsync(model, Prefix);

                if (context.Updater.ModelState.IsValid)
                {
                    settings.PropertyId = model.PropertyId;
                    settings.WidgetId = model.WidgetId;
                }
            }
            return await EditAsync(settings, context);
        }
    }
}
