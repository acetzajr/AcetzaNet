namespace Acetza.Muza.Functions;

public delegate double Transformer(double x);

public static class Transformers
{
    private const double HalfPi = Math.PI / 2;

    public static double Smooth(double x)
    {
        return Math.Sin(HalfPi * x);
    }

    public static double SmoothInverse(double x)
    {
        return Math.Sin(HalfPi * x + HalfPi);
    }
}
