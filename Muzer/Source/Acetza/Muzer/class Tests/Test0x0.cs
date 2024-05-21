using Acetza.Muza.Wavers;

namespace Acetza.Muzer;

static partial class Tests
{
    public static void Test0()
    {
        new Basic().Generate().Save();
    }

    public static void Test1()
    {
        new Enveloper().Generate().Save();
    }
}
