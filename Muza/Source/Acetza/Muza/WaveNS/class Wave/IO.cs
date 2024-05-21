using NAudio.Wave;

namespace Acetza.Muza.WaveNS;

public partial class Wave
{
    public Wave Save(string path = "wave")
    {
        var format = WaveFormat.CreateIeeeFloatWaveFormat(FrameRate.Value, 2);
        using var waveFileWriter = new WaveFileWriter($"{path}.wav", format);
        foreach (var sample in Samples)
        {
            waveFileWriter.WriteSample((float)sample);
        }
        return this;
    }
}
