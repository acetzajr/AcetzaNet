using Acetza.Muza.Wavers;

namespace Acetza.Muzer;

static partial class Tests
{
    public static void Test0()
    {
        var gen = new Basic();
        gen.Generate().Save();
    }

    public static void Test1()
    {
        var gen = new Enveloper();
        gen.Generate().Save();
    }
}
