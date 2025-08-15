using System.Threading.Tasks;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.ContentManagement.Records;
using ThisNetWorks.OrchardCore.GoogleMaps.Indexes;
using YesSql.Sql;

namespace ThisNetWorks.OrchardCore.GoogleMaps;

public class Migrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public Migrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync("GoogleMapPart", builder => builder
            .Attachable()
            .WithDescription("Provides a Google Map part for your content item."));

        return 1;
    }

    public async Task<int> UpdateFrom1Async()
    {
        await SchemaBuilder.CreateMapIndexTableAsync<GoogleMapPartIndex>(table => table
            .Column<string>("ContentType", column => column.WithLength(ContentItemIndex.MaxContentTypeSize))
        );

        // Index on content type as that is most likely to be used for retrieving data from index
        // without having to query document table as well.
        await SchemaBuilder.AlterTableAsync(nameof(GoogleMapPartIndex), table => table
            .CreateIndex("IDX_GoogleMapPartIndex_ContentType", "DocumentId", "ContentType")
        );

        return 2;
    }
}