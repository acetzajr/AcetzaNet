namespace Acetza.Muza.Functions;

public delegate double Amplituder(int index, int number, int depth);

class Amplituders
{
    public static double IndexStandar(int index, int number, int depth)
    {
        return 1f / index;
    }

    public static double Standar(int index, int number, int depth)
    {
        return 1f / number;
    }

    public static double StandarInverse(int index, int number, int depth)
    {
        return 1f - Standar(index, number, depth);
    }

    public static double StandarLinear(int index, int number, int depth)
    {
        return Linear(index, number, depth) * Standar(index, number, depth);
    }

    public static double StandarLinearSmooth(int index, int number, int depth)
    {
        return Linear(index, number, depth)
            * Standar(index, number, depth)
            * Smooth(index, number, depth);
    }

    public static double Linear(int index, int number, int depth)
    {
        var indexDb = (double)index;
        var depthDb = depth + 1f;
        return (depthDb - (indexDb - 1f)) / depthDb;
    }

    public static double StandarSmooth(int index, int number, int depth)
    {
        return Smooth(index, number, depth) * Standar(index, number, depth);
    }

    public static double StandarSmoothInverse(int index, int number, int depth)
    {
        return SmoothInverse(index, number, depth) * Standar(index, number, depth);
    }

    public static double StandarInverseSmoothInverse(int index, int number, int depth)
    {
        return SmoothInverse(index, number, depth) * StandarInverse(index, number, depth);
    }

    public static double Smooth(int index, int number, int depth)
    {
        return Math.Sin(Constants.HalfPi * Linear(index, number, depth));
    }

    public static double SmoothInverse(int index, int number, int depth)
    {
        return Math.Sin(Constants.HalfPi * (1f - Linear(index, number, depth)));
    }

    public static Amplituder Standarizer(double x)
    {
        return (i, n, c) => 1f / Math.Pow(n, x);
    }
}
