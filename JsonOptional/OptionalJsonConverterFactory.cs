using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonOptional;

/// <summary>
/// Creates the correct implementation of <see cref="OptionalJsonConverter{T}"/> for any given type.
/// </summary>
public sealed class OptionalJsonConverterFactory : JsonConverterFactory
{
    private static readonly ConcurrentDictionary<Type, JsonConverter> Cache = new();

    /// <summary>
    /// Determines whether the type can be converted.
    /// </summary>
    /// <param name="typeToConvert">The type is checked as to whether it can be converted.</param>
    /// <returns>True if the type can be converted, false otherwise.</returns>
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType || typeToConvert.GenericTypeArguments.Length != 1)
        {
            return false;
        }

        var t = typeToConvert.GenericTypeArguments[0];
        return typeof(Optional<>).MakeGenericType(t) == typeToConvert;
    }

    /// <summary>
    /// Create a converter for the provided <see cref="Type"/>.
    /// </summary>
    /// <param name="typeToConvert">The <see cref="Type"/> being converted.</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> being used.</param>
    /// <returns>
    /// An instance of a <see cref="JsonConverter{T}"/> where T is compatible with <paramref name="typeToConvert"/>.
    /// If <see langword="null"/> is returned, a <see cref="NotSupportedException"/> will be thrown.
    /// </returns>
    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (Cache.TryGetValue(typeToConvert, out var converter))
        {
            return converter;
        }

        var t = typeToConvert.GenericTypeArguments[0];
        var converterType = typeof(OptionalJsonConverter<>).MakeGenericType(t);
        converter = (JsonConverter) Activator.CreateInstance(converterType);
        Cache.TryAdd(typeToConvert, converter);

        return converter;
    }
}