namespace Acetza.Muza.Functions;

public delegate int Numberer(int i);

public class Numberers
{
    public static int Fib(int i)
    {
        i++;
        while (i >= fibs.Count)
        {
            fibs.Add(fibs[^1] + fibs[^2]);
        }
        return fibs[i];
    }

    public static int Sawtooth(int i)
    {
        return i;
    }

    public static int Square(int i)
    {
        return i * 2 - 1;
    }

    private static readonly List<int> fibs = [0, 1, 1];
}
