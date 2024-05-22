using NAudio.Wave;

namespace Muza.RealTime;

public partial class Session : ISampleProvider
{
    public WaveFormat WaveFormat { get; }

    public int Read(float[] buffer, int offset, int count)
    {
        //Console.WriteLine($"count {count}");
        for (int i = 0; i < count; i++)
        {
            buffer[offset++] = 0;
        }
        if (!_playing)
            return 0;
        return count;
    }
}
