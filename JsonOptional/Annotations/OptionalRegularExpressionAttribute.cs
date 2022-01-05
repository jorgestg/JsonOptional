using System.ComponentModel.DataAnnotations;

namespace JsonOptional.Annotations;

/// <inheritdoc/>
public class OptionalRegularExpressionAttribute : RegularExpressionAttribute
{
    /// <inheritdoc/>
    public OptionalRegularExpressionAttribute(string pattern) : base(pattern)
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