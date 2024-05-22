using Muza.RealTime.Interfaces;

namespace Muza.RealTime.Synths;

public class Basic : ISynth
{
    public void BeginProcess(WaveBuffer.Block block)
    {
        throw new NotImplementedException();
    }

    public void EndProcess(WaveBuffer.Block block)
    {
        throw new NotImplementedException();
    }

    public void NoteOff(string name, int number, int velocity)
    {
        throw new NotImplementedException();
    }

    public void NoteOn(string name, int number, int velocity)
    {
        throw new NotImplementedException();
    }
}
