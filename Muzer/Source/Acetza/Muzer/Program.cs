using Acetza.Muza.Wave;

namespace Acetza.Muzer;

class Program
{
    private static void Main()
    {
        var wave = new Wave(0.001);
        foreach (var iv in wave)
        {
            Console.WriteLine(iv.Index);
            Console.WriteLine(iv.Value);
        }
        Console.WriteLine();
    }
}