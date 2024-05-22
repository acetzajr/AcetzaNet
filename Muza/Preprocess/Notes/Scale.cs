using Rationals;

namespace Muza.Preprocess.Notes;

public class Scale(Rational[] rationals, double baseFrequency = 360)
{
    private readonly Rational[] _rationals = rationals;
    public double Base { get; set; } = baseFrequency;
    public int Count
    {
        get => _rationals.Length;
    }
    public Rational this[int note]
    {
        get => _rationals[note];
        set => _rationals[note] = value;
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
                (Rational)1 / 1,
                (Rational)256 / 243,
                (Rational)9 / 8,
                (Rational)32 / 27,
                (Rational)81 / 64,
                (Rational)4 / 3,
                (Rational)3 / 2,
                (Rational)128 / 81,
                (Rational)27 / 16,
                (Rational)16 / 9,
                (Rational)243 / 128
            ],
            baseFrequency
        );
    }
}
