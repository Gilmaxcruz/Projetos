using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace Mantimentos.App.Configurations
{
    public static class GlobalizationConfig
    {
        public static IApplicationBuilder UseGlobalizationConfig(this IApplicationBuilder app)
        {
            var culturaDefault = new CultureInfo("pt-BR");
            var localizationOption = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culturaDefault),
                SupportedCultures = new List<CultureInfo> { culturaDefault },
                SupportedUICultures = new List<CultureInfo> { culturaDefault }
            };
            app.UseRequestLocalization(localizationOption);

            return app;
        }
    }


}
