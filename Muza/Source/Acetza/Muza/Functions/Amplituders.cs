namespace Acetza.Muza.Functions;

public delegate double Amplituder(int index, int number, int count);

class Amplituders
{
    public static double IndexStandar(int index, int number, int count)
    {
        return 1f / index;
    }

    public static double Standar(int index, int number, int count)
    {
        return 1f / number;
    }

    public static double StandarInverse(int index, int number, int count)
    {
        return 1f - Standar(index, number, count);
    }

    public static double StandarLinear(int index, int number, int count)
    {
        return Linear(index, number, count) * Standar(index, number, count);
    }

    public static double StandarLinearSmooth(int index, int number, int count)
    {
        return Linear(index, number, count)
            * Standar(index, number, count)
            * Smooth(index, number, count);
    }

    public static double Linear(int index, int number, int count)
    {
        var indexDb = (double)index;
        var countDb = count + 1f;
        return (countDb - (indexDb - 1f)) / countDb;
    }

    public static double StandarSmooth(int index, int number, int count)
    {
        return Smooth(index, number, count) * Standar(index, number, count);
    }

    public static double StandarSmoothInverse(int index, int number, int count)
    {
        return SmoothInverse(index, number, count) * Standar(index, number, count);
    }

    public static double StandarInverseSmoothInverse(int index, int number, int count)
    {
        return SmoothInverse(index, number, count) * StandarInverse(index, number, count);
    }

    public static double Smooth(int index, int number, int count)
    {
        return Math.Sin(Constants.HalfPi * Linear(index, number, count));
    }

    public static double SmoothInverse(int index, int number, int count)
    {
        return Math.Sin(Constants.HalfPi * (1f - Linear(index, number, count)));
    }

    public static Amplituder Standarizer(double x)
    {
        return (i, n, c) => 1f / Math.Pow(n, x);
    }
}
