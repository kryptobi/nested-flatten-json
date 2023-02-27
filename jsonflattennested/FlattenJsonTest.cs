using System.Globalization;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit;
using System.IO;

namespace jsonflattennested;

public class FlattenJsonTest
{
    private readonly string JsonString;

    public FlattenJsonTest()
    {
        JsonString =  """
{
     "name": "John",
     "address":{
          "city": "Lisbon",
          "street": "Central street"
     }
}
""";
    }

    [Fact]
    public void flatThatJsonString()
    {
        var flattedJsonDict = FlatNestedJson(JsonString);
        flattedJsonDict.TryGetValue("address.city", out var value);

        value.Should().NotBeNull();
        value.Should().Be("Lisbon");
    }
    
    [Fact]
    public async Task flatThatJsonStringInFile()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "File.json");
        var readJson = await File.ReadAllTextAsync(filePath);
        var flattedJsonDict = FlatNestedJson(readJson);
        flattedJsonDict.TryGetValue("address.city", out var value);

        value.Should().NotBeNull();
        value.Should().Be("Lisbon");
    }

    private Dictionary<string, string> FlatNestedJson(string json)
    {
        return  JObject.Parse(json)
                       .Descendants()
                       .OfType<JValue>()
                       .ToDictionary(jv => jv.Path, jv => jv.ToString(CultureInfo.InvariantCulture));
    }
}