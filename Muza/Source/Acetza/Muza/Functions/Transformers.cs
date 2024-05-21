namespace Acetza.Muza.Functions;

public delegate double Transformer(double x);

public static class Transformers
{
    public static double Smooth(double x)
    {
        return Math.Sin(Constants.HalfPi * x);
    }

    public static double SmoothInverse(double x)
    {
        return Math.Sin(Constants.HalfPi * x + Constants.HalfPi);
    }
}
