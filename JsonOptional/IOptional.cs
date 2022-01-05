namespace JsonOptional;

/// <summary>
/// Non-generic wrapper for <see cref="Optional{T}"/>
/// </summary>
public interface IOptional
{
    /// <inheritdoc cref="Optional{T}.HasValue"/>
    bool HasValue { get; }

    /// <inheritdoc cref="Optional{T}.Value"/>
    object Value { get; }

    /// <inheritdoc cref="Optional{T}.ValueOrDefault"/>
    object? ValueOrDefault();
}