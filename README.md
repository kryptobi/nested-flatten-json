# Find your value in a json string by a dotted key

### json value finder by json key which have to be flatted

First the nested json string will be flatted to a dictionary of key and value. The nested keys will added to each other by a dot.

Exampe:
```csharp
{
  "name": "John",
  "address": {
    "city": "Lisbon",
    "street": "Central street"
  }
}
```
will be flatten to dictionary

<img width="452" alt="Bildschirm­foto 2023-02-27 um 22 05 06" src="https://user-images.githubusercontent.com/10707524/221685441-03f6cbad-80d3-499c-9e20-531fd3da139e.png">

by the method:

```csharp
return JObject.Parse(json)
              .Descendants()
              .OfType<JValue>()
              .ToDictionary(jv => jv.Path, jv => jv.ToString(cultureInfo));
```
As you can see in the class JsonKeyValueProvider.

To find a value you can easily use the TryGetValue Method from a dictionary.

```charp
flattedJsonDict.TryGetValue("address.city", out var value);

value.Should().Be("Lisbon");
```

The value should be "Lisbon"
