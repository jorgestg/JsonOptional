![JsonOptional](./logo.png)

# JsonOptional

Allows you to represent values that might be missing in a JSON document in .NET.

## Examples

Deserialize using `Optional`s:

```c#
using System;
using System.Text.Json;
using JsonOptional;

const string json = "{ \"FirstName\": \"John\", \"LastName\": \"Doe\" }";

var up = JsonSerializer.Deserialize<Person>(json, new JsonSerializerOptions
{
    Converters = { new OptionalJsonConverterFactory() }
});

Console.WriteLine(p.FirstName.Value == "John");
Console.WriteLine(p.MiddleName.HasValue);
Console.WriteLine(p.LastName.Value == "Doe");

// Result:
//   True
//   False
//   True

public class Person 
{
    public Optional<string> FirstName { get; set; }
    public Optional<string> MiddleName { get; set; }
    public Optional<string> LastName { get; set; }
}
```

Using with ASP.NET Core for merge patching:

```c#
// Startup.cs or Program.cs
using JsonOptional;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// ...

services
    .AddControllers(opts =>
    {
        opts.ModelMetadataDetailsProviders.Add(
            new SuppressChildValidationMetadataProvider(typeof(IOptional)));
    })
    .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new OptionalJsonConverterFactory()));
```

```c#
using JsonOptional;
using Microsoft.AspNetCore.Mvc;

public class Person 
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
}

public class UpdatePerson 
{
    public Optional<string> FirstName { get; set; }
    public Optional<string> MiddleName { get; set; }
    public Optional<string> LastName { get; set; }
}

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private static readonly List<Person> People = new()
    {
        new() { FirstName = "John", MiddleName = "Jay", LastName = "Do" },
    };

    [HttpGet]
    public List<Person> Get()
    {
        return People;
    }

    [HttpPost]
    public Person Add(Person p)
    {
        People.Add(p);
        return p;
    }

    [HttpPatch("{i:int}")]
    public ActionResult<Person> Patch(int i, UpdatePerson up)
    {
        var p = People.ElementAtOrDefault(i);
        if (p is null)
        {
            return NotFound();
        }

        if (up.FirstName.HasValue) p.FirstName = up.FirstName.Value;
        if (up.MiddleName.HasValue) p.MiddleName = up.MiddleName.Value;
        if (up.LastName.HasValue) p.LastName = up.LastName.Value;

        return Ok(p);
    }
}
```

## Annotations

`JsonOptional.Annotations` adds `Optional`-friendly alternatives to the most common data and validation annotations.

```c#
[OptionalNotNull]
[OptionalStringLength(200, Minimum = 1)]
public Optional<string> FirstName { get; init; }

[OptionalNotNull]
[OptionalUrl]
public Optional<string> Website { get; init; }

[OptionalNotNull]
[OptionalEmail]
public Optional<string> Email { get; init; }
```