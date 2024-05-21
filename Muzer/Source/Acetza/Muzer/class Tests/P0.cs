using Acetza.Muza.Functions;
using Acetza.Muza.Wavers;

namespace Acetza.Muzer;

static partial class Tests
{
    public static void Basic()
    {
        new Basic().Generate().Save();
    }

    public static void Enveloper()
    {
        new Enveloper().Generate().Save();
    }

    public static void Harmonizer()
    {
        new Harmonizer().Generate().Save();
    }

    public static void Test1()
    {
        var waver = new Enveloper(waver: new Basic(primitive: Primitives.Tri));
        var wave = waver.Generate();
        waver.Frequency *= 2.0 / 3.0;
        wave.Add(waver.Generate()).Normalize().Save();
    }
}
