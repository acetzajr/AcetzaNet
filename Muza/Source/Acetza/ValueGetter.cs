namespace Acetza;
public interface IValueGetter<T>
{
    T Value { get; }
}
public interface IValueSetter<T>
{
    T Value { set; }
}