using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Resource;

public static class ServiceRegistration
{

    public static IServiceCollection AddConfigureLocalization(this IServiceCollection services)
    {

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new List<CultureInfo>
                  {
                    new CultureInfo("tr"),
                    new CultureInfo("en"),
                  };

            options.DefaultRequestCulture = new RequestCulture("tr");
            options.SupportedUICultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider()
            {
                Options = options
            });
        });

        return services;
    }
}
