using System.Diagnostics;
using Acetza.Muzer;

namespace Muzer.Source.Acetza.Muzer;

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

    [STAThread]
    private static void Main()
    {
        Measure(Tests.Selected);
    }
}
