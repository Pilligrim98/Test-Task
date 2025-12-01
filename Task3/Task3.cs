using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Task3;

class Task3
{
    private static JsonSerializerOptions _jsonSerializerOptions = 
        new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

    static void Main(string[] args)
    {
        var valuesPath = args[0];
        var testsPath = args[1];
        var reportPath = args[2];

        var valuesWrapper = GetValues(valuesPath);
        
        var testsWrapper = GetTests(testsPath);
        var dictionary = testsWrapper.GetTestsDictionary();

        foreach (var valueObj in valuesWrapper.Values)
        {
            if (dictionary.TryGetValue(valueObj.Id, out var test))
            {
                test.Value = valueObj.Value;
            }
        }
        
        var result = JsonSerializer.Serialize(testsWrapper, _jsonSerializerOptions);
        File.WriteAllText(reportPath, result);
    }

    private static ValueObjWrapper GetValues(string valuesPath)
    {
        var content = File.ReadAllText(valuesPath);
        var values = JsonSerializer.Deserialize<ValueObjWrapper>(content, _jsonSerializerOptions);

        return values;
    }

    private static TestsWrapper GetTests(string testsPath)
    {
        var content = File.ReadAllText(testsPath);
        var testsWrapper = JsonSerializer.Deserialize<TestsWrapper>(content, _jsonSerializerOptions);

        return testsWrapper;
    }
}

public class Test
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Value { get; set; }
    public Test[] Values { get; set; }

    public void AddToDictionary(Dictionary<int,Test> testsIdDictionary)
    {
        testsIdDictionary.Add(Id, this);
        
        if (Values == null)
        {
            return;
        }
        
        foreach (var test in Values)
        {
            test.AddToDictionary(testsIdDictionary);
        }
    }
}

public class TestsWrapper
{
    public Test[] Tests { get; set; }

    public Dictionary<int, Test> GetTestsDictionary()
    {
        var testsIdDictionary = new Dictionary<int, Test>();

        foreach (var test in Tests)
        {
            // var innerTests = test.GetAllInnerTests();
            // foreach (var innerTest in innerTests)
            // {
            //     testsIdDictionary.Add(innerTest.Id, innerTest);
            // }

            test.AddToDictionary(testsIdDictionary);
        }

        return testsIdDictionary;
    }
}

public class ValueObj
{
    public int Id { get; set; }
    public string Value { get; set; }
}

public class ValueObjWrapper
{
    public ValueObj[] Values { get; set; }
}