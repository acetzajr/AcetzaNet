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
}
