namespace JsonOptional;

/// <summary>
/// A wrapper object which may or may not contain a value.
/// If a value is present, <see cref="HasValue"/> will return true and <see cref="Value"/> will return the value.
/// </summary>
/// <typeparam name="T">The type of the value</typeparam>
public readonly struct Optional<T> : IOptional
{
    private readonly T _value;

    /// <summary>
    /// Creates an <see cref="Optional{T}"/> that wraps a value.
    /// </summary>
    /// <param name="value">The value.</param>
    public Optional(T value)
    {
        HasValue = true;
        _value = value;
    }

    /// <summary>
    /// Gets whether the <see cref="Optional{T}"/> is wrapping a value or not.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// Gets the value of the <see cref="Optional{T}"/> if <see cref="HasValue"/> is true.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     <see cref="HasValue"/> is false.
    /// </exception>
    public T Value => HasValue ? _value : throw new InvalidOperationException("Optional has no value");

    object IOptional.Value => Value!;

    /// <summary>
    /// Gets the value of the <see cref="Optional{T}"/> if <see cref="HasValue"/> is true,
    /// otherwise returns the default value of T.
    /// </summary>
    /// <returns>
    ///     <see cref="Value"/> or the default value of T.
    /// </returns>
    public T? ValueOrDefault()
        => HasValue ? Value : default;

    object? IOptional.ValueOrDefault()
        => ValueOrDefault();

    /// <summary>
    /// Creates an <see cref="Optional{T}"/> that wraps the assigned value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>An optional with the value.</returns>
    public static implicit operator Optional<T>(T value)
        => new(value);
}

/// <summary>
/// Contains methods for creating <see cref="Optional{T}"/>.
/// </summary>
public static class Optional
{
    /// <summary>
    /// Creates an <see cref="Optional{T}"/> that wraps a value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>An optional with the value.</returns>
    public static Optional<T> FromValue<T>(T value)
        => new(value);
}