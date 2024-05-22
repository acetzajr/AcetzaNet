namespace Muza.RealTime.Interfaces;

public interface ISynth : IMidiHandler
{
    public void BeginProcess(WaveBuffer.Block block);
    public void EndProcess(WaveBuffer.Block block);
}
