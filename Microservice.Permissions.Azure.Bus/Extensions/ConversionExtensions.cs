using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microservice.Permissions.Azure.Bus.Extensions;

public static class ConversionExtensions
{
    public static string Serialize<T>(this T source)
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        var result = JsonConvert.SerializeObject(source, settings);
        return result;
    }

    public static T? Deserialize<T>(this string source)
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        var result = JsonConvert.DeserializeObject<T>(source, settings);
        return result;
    }

    public static ReadOnlyMemory<byte> ToBytes(this string source)
    {
        var result = Encoding.UTF8.GetBytes(source);
        return result;
    }

    public static string FromBytes(this ReadOnlyMemory<byte> source)
    {
        var result = Encoding.UTF8.GetString(source.ToArray());
        return result;
    }
}