using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ThisNetWorks.OrchardCore.GoogleMaps;

public static class CamelCaseJsonSerializer
{
    public static readonly JsonSerializerSettings Settings = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };
    public static readonly JsonSerializer Serializer = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };
}