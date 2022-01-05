using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonOptional;

/// <inheritdoc/>
public sealed class OptionalJsonConverter<T> : JsonConverter<Optional<T?>>
{
    /// <inheritdoc/>
    public override Optional<T?> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = JsonSerializer.Deserialize<T?>(ref reader, options);
        return new Optional<T?>(value);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Optional<T?> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.ValueOrDefault(), options);
    }
}