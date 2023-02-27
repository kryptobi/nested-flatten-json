using System.Globalization;
using Newtonsoft.Json.Linq;

namespace jsonflattennested;

public class JsonKeyValueProvider : IJsonKeyValueProvider
{
    public Dictionary<string, string> FlatNestedJson(
        string json, 
        CultureInfo? cultureInfo = null)
    {
        if (string.IsNullOrWhiteSpace(json)) return new Dictionary<string, string>();
        cultureInfo ??= CultureInfo.InvariantCulture;

        return JObject.Parse(json)
                      .Descendants()
                      .OfType<JValue>()
                      .ToDictionary(jv => jv.Path, jv => jv.ToString(cultureInfo));
    }

    public async Task<Dictionary<string, string>> FlatNestedJsonByJsonFile(
        string jsonFilePath, 
        CultureInfo? cultureInfo = null)
    {
        var filePath = Path.Combine(jsonFilePath);
        var readJson = await File.ReadAllTextAsync(filePath);
        
        return FlatNestedJson(readJson, cultureInfo);
    }
}