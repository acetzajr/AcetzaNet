namespace Muza.Notes;

public class Scale(double[] rations, double baseFrequency = 360)
{
    private readonly double[] _rations = rations;
    public double Base { get; set; } = baseFrequency;
    public int Count
    {
        get => _rations.Length;
    }
    public double this[int note]
    {
        get => _rations[note];
        set => _rations[note] = value;
    }

    public int Note(int index)
    {
        var x = index;
        var m = Count;
        return x < 0 ? (x % m + m) % m : x % m;
    }

    public double Power(int index)
    {
        return index < 0 ? (index + 1) / Count - 1 : index / Count;
    }

    public double Frequency(int note)
    {
        return Base * (double)this[Note(note)] * Math.Pow(2, Power(note));
    }

    public static Scale Acetza(double baseFrequency = 360)
    {
        return new Scale(
            [
                (double)1 / 1,
                (double)256 / 243,
                (double)9 / 8,
                (double)32 / 27,
                (double)81 / 64,
                (double)4 / 3,
                EqualTempered.Frequency(6, baseFrequency: 1.0),
                (double)3 / 2,
                (double)128 / 81,
                (double)27 / 16,
                (double)16 / 9,
                (double)243 / 128
            ],
            baseFrequency
        );
    }
}
