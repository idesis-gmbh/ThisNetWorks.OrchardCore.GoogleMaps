using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrchardCore.Recipes.Services;
using OrchardCore.Setup.Events;
using System.Linq;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Sample;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOrchardCms(builder => builder
            .ConfigureServices(s => {
                s.AddScoped<ISetupEventHandler, SetupGoogleMapsSampleSiteEvent>();
                var harvester = s.FirstOrDefault(x => x.ServiceType == typeof(IRecipeHarvester) && x.ImplementationType == typeof(RecipeHarvester));
                s.Remove(harvester);
                s.AddScoped<IRecipeHarvester, RestrictedRecipeHarvestor>();
            }, 100)
        );
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseOrchardCore();
    }
}