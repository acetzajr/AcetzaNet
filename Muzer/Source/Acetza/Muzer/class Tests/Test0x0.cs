using Acetza.Muza.Wavers;

namespace Acetza.Muzer;

static partial class Tests
{
    public static void Test0x0()
    {
        var gen = new Basic();
        gen.Generate().Save();
    }
}