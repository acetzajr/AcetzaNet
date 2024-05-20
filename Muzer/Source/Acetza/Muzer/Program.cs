using System.Diagnostics;

namespace Acetza.Muzer;

public delegate void TestFn();

class Program
{
    static void Measure(TestFn fn)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        fn();
        stopwatch.Stop();
        Console.WriteLine($"Running time: {stopwatch.Elapsed}");
    }
    private static void Main()
    {
        Measure(Tests.Test0x0);
    }
}