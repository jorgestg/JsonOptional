using System.ComponentModel.DataAnnotations;

namespace JsonOptional.Annotations;

/// <summary>
/// Validates that an <see cref="Optional{T}"/>'s value is not null.
/// </summary>
public class OptionalNotNullAttribute : ValidationAttribute
{
    /// <summary>
    /// Creates a new instance of the <see cref="OptionalNotNullAttribute"/> class.
    /// </summary>
    public OptionalNotNullAttribute()
    {
        ErrorMessage = "{0} must not be null";
    }

    /// <inheritdoc/>
    public override bool IsValid(object value)
    {
        if (value is not IOptional o)
        {
            return false;
        }

        return !o.HasValue || (object?) o.Value is not null;
    }
}