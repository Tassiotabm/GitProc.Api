using GitProc.Api.Extensions.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace GitProc.Api.Extensions
{
    public static class GlobalizationExtension
    {
        public static void AddGlobalization(this IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizerFactory, EFStringLocalizerFactory>();
            services.AddTransient(typeof(IStringLocalizer<>), typeof(EFStringLocalizer<>));
            services.AddTransient(typeof(IStringLocalizer), typeof(EFStringLocalizer));
        }

        public static void UseGlobalization(this IApplicationBuilder app)
        {

            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("pt-BR"),
                new CultureInfo("en-US")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            // Optionally create an app-specific provider with just a delegate, e.g. look up user preference from DB.
            // Inserting it as position 0 ensures it has priority over any of the default providers.
            //options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
            //{

            //}));

            app.UseRequestLocalization(options);
        }
    }
}
