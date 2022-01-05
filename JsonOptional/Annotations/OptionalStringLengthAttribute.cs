using System.ComponentModel.DataAnnotations;

namespace JsonOptional.Annotations;

/// <inheritdoc/>
public class OptionalStringLengthAttribute : StringLengthAttribute
{
    /// <inheritdoc/>
    public OptionalStringLengthAttribute(int maximumLength) : base(maximumLength)
    {
    }

    /// <inheritdoc/>
    public override bool IsValid(object? value)
    {
        if (value is not Optional<string> o)
        {
            return false;
        }

        return !o.HasValue || base.IsValid(o.Value);
    }
}