using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace JsonFlattenNested;

public class JsonKeyValueProvider : IJsonKeyValueProvider
{
    public Dictionary<string, string> FlatNestedJson(
        string json, 
        CultureInfo cultureInfo = null)
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
        CultureInfo cultureInfo = null)
    {
        var filePath = Path.Combine(jsonFilePath);
        var readJson = await File.ReadAllTextAsync(filePath);
        
        return FlatNestedJson(readJson, cultureInfo);
    }
}