using System.ComponentModel.DataAnnotations;

namespace JsonOptional.Annotations;

/// <inheritdoc/>
public class OptionalMaxLengthAttribute : MaxLengthAttribute
{
    /// <inheritdoc/>
    public OptionalMaxLengthAttribute()
    {
    }

    /// <inheritdoc/>
    public OptionalMaxLengthAttribute(int length) : base(length)
    {
    }

    /// <inheritdoc/>
    public override bool IsValid(object? value)
    {
        if (value is not IOptional o)
        {
            return false;
        }

        return !o.HasValue || base.IsValid(o.Value);
    }
}