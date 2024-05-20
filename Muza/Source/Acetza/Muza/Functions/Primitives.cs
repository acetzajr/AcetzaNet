namespace Acetza.Muza.Functions;

public delegate double Primitive(double x);

public static class Primitives
{
    public static double Sin(double x)
    {
        return Math.Sin(2 * Math.PI * x);
    }

    public static double Tri(double x)
    {
        if (x < 0.25)
        {
            return 4.0 * x;
        }
        if (x < 0.75)
        {
            return 2.0 - 4.0 * x;
        }
        return 4.0 * x - 4.0;
    }

    public static double Sqr(double x)
    {
        return x < 0.5 ? 1.0 : -1.0;
    }

    public static double Saw(double x)
    {
        return 1.0 - 2.0 * x;
    }
}