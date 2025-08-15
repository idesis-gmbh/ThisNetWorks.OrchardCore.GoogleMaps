using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using System.Threading.Tasks;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Settings
{
    public class GoogleMapsSettingsDisplayDriver : SectionDisplayDriver<ISite, GoogleMapsSettings>
    {
        public const string GroupId = "googlemaps";

        public override Task<IDisplayResult> EditAsync(ISite model, GoogleMapsSettings settings, BuildEditorContext context)
        {
            var result = (IDisplayResult)Initialize<GoogleMapsSettingsViewModel>("GoogleMapsSettings_Edit", model =>
            {
                model.ApiKey = settings.ApiKey;
                model.Location = settings.Location;
                model.DefaultMarker = settings.DefaultMarker;
                model.GoogleMapSettings = settings;

            }).Location("Content").OnGroup(GroupId);

            return Task.FromResult(result);
        }

        public override async Task<IDisplayResult> UpdateAsync(ISite siteSettings, GoogleMapsSettings settings, UpdateEditorContext context)
        {
            if (context.GroupId == GroupId)
            {
                var model = new GoogleMapsSettingsViewModel();

                if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.ApiKey, m => m.Location, m => m.DefaultMarker ))
                {
                    settings.ApiKey = model.ApiKey;
                    settings.Location = model.Location;
                    settings.DefaultMarker = model.DefaultMarker;
                }
            }

            return await base.EditAsync(siteSettings, context);
        }
    }
}