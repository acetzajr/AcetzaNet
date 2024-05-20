namespace Acetza;

public readonly struct IndexedValue<T>(int index, T value)
{
    public int Index { get; } = index;
    public T Value { get; } = value;
}
