using System.ComponentModel.DataAnnotations;

namespace JsonOptional.Annotations;

/// <inheritdoc cref="EmailAddressAttribute"/>
public class OptionalEmailAddressAttribute : DataTypeAttribute
{
    private static readonly EmailAddressAttribute Attribute = new();

    /// <inheritdoc cref="EmailAddressAttribute()"/>
    public OptionalEmailAddressAttribute() : base(DataType.EmailAddress)
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