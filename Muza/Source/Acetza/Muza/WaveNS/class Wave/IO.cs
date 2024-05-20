using NAudio.Wave;

namespace Acetza.Muza.WaveNS;

public partial class Wave
{
    public Wave Save(string path = "wave")
    {
        var format = WaveFormat.CreateIeeeFloatWaveFormat(FrameRate.Value, 2);
        using var waveFileWriter = new WaveFileWriter($"{path}.wav", format);
        foreach (var indexedFrame in this)
        {
            foreach (var indexedChannel in indexedFrame.Value)
            {
                waveFileWriter.WriteSample((float)indexedChannel.Value);
            }
        }
        return this;
    }
}
