using NAudio.Wave;

namespace Muza.RealTime;

public partial class Session : ISampleProvider
{
    public WaveFormat WaveFormat { get; }

    public int Read(float[] buffer, int offset, int count)
    {
        if (_waveBuffer is null)
            return 0;
        for (int i = 0; i < count; i++)
        {
            buffer[offset++] = (float)_waveBuffer.NextSample();
        }
        if (!_playing)
            return 0;
        return count;
    }
}
