using System.Globalization;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit;
using System.IO;

namespace jsonflattennested;

public class FlattenJsonTest
{
    private readonly string JsonString;
    private readonly IJsonKeyValueProvider _jsonKeyValueProvider;

    public FlattenJsonTest()
    {
        _jsonKeyValueProvider = new JsonKeyValueProvider();
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
        var flattedJsonDict = _jsonKeyValueProvider.FlatNestedJson(JsonString);
        flattedJsonDict.TryGetValue("address.city", out var value);

        value.Should().NotBeNull();
        value.Should().Be("Lisbon");
    }
    
    [Fact]
    public async Task flatThatJsonStringInFile()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "File.json");
        var flattedJsonDict = await _jsonKeyValueProvider.FlatNestedJsonByJsonFile(filePath);
        flattedJsonDict.TryGetValue("address.city", out var value);

        value.Should().NotBeNull();
        value.Should().Be("Lisbon");
    }
}