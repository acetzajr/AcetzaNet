using NAudio.Wave;

namespace Acetza.Muza.WaveNS;

public partial class Wave
{
    public Wave Save(string path = "wave")
    {
        var format = WaveFormat.CreateIeeeFloatWaveFormat(FrameRate.Value, 2);
        using var waveFileWriter = new WaveFileWriter($"{path}.wav", format);
        foreach (var iv in this)
        {
            waveFileWriter.WriteSample((float)iv.Value.Left);
            waveFileWriter.WriteSample((float)iv.Value.Right);
        }
        return this;
    }
}