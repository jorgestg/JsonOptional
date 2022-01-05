using System.ComponentModel.DataAnnotations;

namespace JsonOptional.Annotations;

/// <inheritdoc cref="UrlAttribute"/>
public class OptionalUrlAttribute : DataTypeAttribute
{
    private static readonly UrlAttribute Attribute = new();

    /// <inheritdoc cref="UrlAttribute()"/>
    public OptionalUrlAttribute() : base(DataType.Url)
    {
        ErrorMessage = Attribute.ErrorMessage;
    }

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        if (value is not Optional<string> o)
        {
            return false;
        }

        return !o.HasValue || Attribute.IsValid(o.Value);
    }
}