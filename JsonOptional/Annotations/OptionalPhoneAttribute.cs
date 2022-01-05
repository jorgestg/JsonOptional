using System.ComponentModel.DataAnnotations;

namespace JsonOptional.Annotations;

/// <inheritdoc cref="PhoneAttribute"/>
public class OptionalPhoneAttribute : DataTypeAttribute
{
    private static readonly PhoneAttribute Attribute = new();

    /// <inheritdoc cref="PhoneAttribute()"/>
    public OptionalPhoneAttribute() : base(DataType.EmailAddress)
    {
        ErrorMessage = Attribute.ErrorMessage;
    }

    /// <inheritdoc/>
    public override bool IsValid(object? value)
    {
        if (value is not Optional<string> o)
        {
            return false;
        }

        return !o.HasValue || Attribute.IsValid(o.Value);
    }
}