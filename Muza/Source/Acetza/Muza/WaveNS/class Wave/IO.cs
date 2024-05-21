using NAudio.Wave;

namespace Acetza.Muza.WaveNS;

public partial class Wave
{
    public Wave Save(string path = "wave")
    {
        var format = WaveFormat.CreateIeeeFloatWaveFormat(FrameRate.Value, ChannelsCount);
        using var waveFileWriter = new WaveFileWriter($"{path}.wav", format);
        foreach (var sample in Samples)
        {
            waveFileWriter.WriteSample((float)sample);
        }
        return this;
    }

    public Wave Play()
    {
        Console.WriteLine("Playing");
        using (var outputDevice = new WaveOutEvent())
        {
            outputDevice.Init(this);
            outputDevice.Play();
            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1_000);
            }
        }
        return this;
    }
}
