using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.Admin;
using OrchardCore.TawkTo.Settings;
using OrchardCore.Entities;
using OrchardCore.ResourceManagement;
using OrchardCore.Settings;

namespace OrchardCore.TawkTo
{
    public class TawkToFilter : IAsyncResultFilter
    {
        private readonly IResourceManager resourceManager;
        private readonly ISiteService siteService;

        private HtmlString scriptsCache;

        public TawkToFilter(IResourceManager resourceManager, ISiteService siteService)
        {
            this.resourceManager = resourceManager;
            this.siteService = siteService;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // Should only run on the front-end for a full view
            if ((context.Result is ViewResult || context.Result is PageResult) && !AdminAttribute.IsApplied(context.HttpContext))
            {
                if (this.scriptsCache == null)
                {
                    var settings = (await this.siteService.GetSiteSettingsAsync()).As<TawkToSettings>();
                    if (!string.IsNullOrEmpty(settings.PropertyId) && !string.IsNullOrEmpty(settings.WidgetId))
                    {
                        this.scriptsCache = new HtmlString($"<script src=\"/OrchardCore.TawkTo/scripts/tawk-to.min.js\" pid=\"{settings.PropertyId}\" wid=\"{settings.WidgetId}\"></script>");
                    }
                }

                if (this.scriptsCache != null)
                {
                    this.resourceManager.RegisterHeadScript(this.scriptsCache);
                }
            }

            await next.Invoke();
        }
    }
}
