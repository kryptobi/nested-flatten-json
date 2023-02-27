using FluentAssertions;
using Xunit;

namespace JsonFlattenNested.Tests;

public class FlattenJsonTest
{
    private readonly string _jsonString;
    private readonly IJsonKeyValueProvider _jsonKeyValueProvider;

    public FlattenJsonTest()
    {
        _jsonKeyValueProvider = new JsonKeyValueProvider();
        _jsonString = """
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
    public void FlatThatJsonString()
    {
        var flattedJsonDict = _jsonKeyValueProvider.FlatNestedJson(_jsonString);
        flattedJsonDict.TryGetValue("address.city", out var value);

        value.Should().NotBeNull();
        value.Should().Be("Lisbon");
    }

    [Fact]
    public async Task FlatThatJsonStringInFileAsync()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "File.json");
        var flattedJsonDict = await _jsonKeyValueProvider.FlatNestedJsonByJsonFile(filePath);
        flattedJsonDict.TryGetValue("address.city", out var value);

        value.Should().NotBeNull();
        value.Should().Be("Lisbon");
    }
}