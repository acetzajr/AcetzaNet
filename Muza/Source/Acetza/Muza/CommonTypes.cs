namespace Acetza.Muza;

public class Sample(double value) : IValueGetter<double>, IValueSetter<double>
{
    public double Value { get; set; } = value;
}