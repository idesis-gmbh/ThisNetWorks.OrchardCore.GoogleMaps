using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;
using ThisNetWorks.OrchardCore.GoogleMaps.Settings;

namespace ThisNetWorks.OrchardCore.GoogleMaps;

public class AdminMenu : INavigationProvider
{
    private readonly IStringLocalizer _s;
    public AdminMenu(IStringLocalizer<AdminMenu> localizer)
    {
        _s = localizer;
    }


    public ValueTask BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
        {
            return ValueTask.CompletedTask;
        }

        builder
            .Add(_s["Configuration"], configuration => configuration
                .Add(_s["Settings"], settings => settings
                    .Add(_s["Google Maps"], _s["Google Maps"], layers => layers
                        .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = GoogleMapsSettingsDisplayDriver.GroupId })
                        .Permission(Permissions.ManageGoogleMaps)
                        .LocalNav()
                    )));

        return ValueTask.CompletedTask;
    }
}