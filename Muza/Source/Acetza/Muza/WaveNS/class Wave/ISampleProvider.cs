using NAudio.Wave;

namespace Acetza.Muza.WaveNS;

public partial class Wave : ISampleProvider
{
    public class SampleIndex
    {
        public int Frame { get; set; }
        public int Channel { get; set; }
    }

    public SampleIndex PlayIndex { get; set; } = new SampleIndex { Frame = 0, Channel = 0 };
    public WaveFormat WaveFormat { get; }

    public int Read(float[] buffer, int offset, int count)
    {
        int read = 0;
        for (; PlayIndex.Frame < FramesCount; PlayIndex.Frame++)
        {
            for (; PlayIndex.Channel < ChannelsCount; PlayIndex.Channel++)
            {
                buffer[offset++] = (float)this[PlayIndex.Frame][PlayIndex.Channel];
                read++;
                if (read == count)
                    goto Exit;
            }
            PlayIndex.Channel = 0;
        }
        Exit:
        return read;
    }
}
