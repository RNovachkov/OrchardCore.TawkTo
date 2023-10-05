using Microsoft.Extensions.Options;
using OrchardCore.TawkTo.Settings;
using OrchardCore.TawkTo.ViewModels;
using OrchardCore.Entities;
using OrchardCore.Recipes.Models;
using OrchardCore.Recipes.Services;
using OrchardCore.Settings;
using System.Threading.Tasks;

namespace OrchardCore.TawkTo.Recipes
{
    public class TawkToSettingsStep : IRecipeStepHandler
    {
        private readonly ISiteService siteService;
        private readonly TawkToSettings defaultSettings;

        public TawkToSettingsStep(ISiteService siteService, IOptions<TawkToSettings> options)
        {
            this.siteService = siteService;
            this.defaultSettings = options.Value;
        }

        public async Task ExecuteAsync(RecipeExecutionContext context)
        {
            if (!string.Equals(context.Name, nameof(TawkToSettings), StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            var model = context.Step.ToObject<TawkToSettingsViewModel>();
            var container = await this.siteService.LoadSiteSettingsAsync();
            container.Alter<TawkToSettings>(nameof(TawkToSettings), aspect =>
            {
                aspect.PropertyId = model.PropertyId ?? this.defaultSettings.PropertyId;
                aspect.WidgetId = model.WidgetId ?? this.defaultSettings.WidgetId;
            });
            await this.siteService.UpdateSiteSettingsAsync(container);
        }
    }
}
