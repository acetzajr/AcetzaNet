using Muza.RealTime.Synths;

namespace Muza.RealTime;

public class Testing
{
    public static void Test()
    {
        using var session = new Session();
        session.AddSynth(new Basic());
        session.Start();
    }
}
