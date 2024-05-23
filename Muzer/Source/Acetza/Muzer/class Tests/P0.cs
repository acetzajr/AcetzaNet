using Muza.RealTime;

namespace Acetza.Muzer;

public delegate void TestFn();

static partial class Tests
{
    public static TestFn Selected = Testing.Test;
    public static Semaphore semaphore = new(0, 3);

    public static void Test()
    {
        ThreadPool.QueueUserWorkItem(CallBack);
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Releasing {i}...");
            Console.ReadLine();
            semaphore.Release();
        }
        Console.ReadLine();
    }

    public static void CallBack(object? state)
    {
        for (int i = 0; i < 3; i++)
        {
            semaphore.WaitOne();
            Console.WriteLine($"{i} released!");
        }
        Console.WriteLine("Released finally!");
    }
}
