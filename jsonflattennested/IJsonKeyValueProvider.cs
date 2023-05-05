using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace JsonFlattenNested;

public interface IJsonKeyValueProvider
{
    public Dictionary<string, string> FlatNestedJson(string json,
                                                     CultureInfo cultureInfo = null);
    
    public Task<Dictionary<string, string>> FlatNestedJsonByJsonFile(string jsonFilePath,
                                                                     CultureInfo cultureInfo = null);
}