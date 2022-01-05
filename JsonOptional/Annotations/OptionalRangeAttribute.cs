using System.ComponentModel.DataAnnotations;

namespace JsonOptional.Annotations;

/// <inheritdoc/>
public class OptionalRangeAttribute : RangeAttribute
{
    /// <inheritdoc/>
    public OptionalRangeAttribute(double minimum, double maximum) : base(minimum, maximum)
    {
    }

    /// <inheritdoc/>
    public OptionalRangeAttribute(int minimum, int maximum) : base(minimum, maximum)
    {
    }

    /// <inheritdoc/>
    public OptionalRangeAttribute(Type type, string minimum, string maximum) : base(type, minimum, maximum)
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