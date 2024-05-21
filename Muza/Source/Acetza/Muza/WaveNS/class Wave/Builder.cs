using NAudio.Wave;

namespace Acetza.Muza.WaveNS;

public partial class Wave
{
    public Wave(int channels = 2, int frameRate = 44_100)
    {
        ChannelsCount = channels;
        FrameRate = new FrameRate(frameRate);
        _frames = [];
        WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(FrameRate.Value, ChannelsCount);
    }

    public Wave(double duration, int channels = 2, int frameRate = 44_100)
    {
        ChannelsCount = channels;
        FrameRate = new FrameRate(frameRate);
        var framesCount = FrameRate.TimeToIndex(duration);
        _frames = new List<Frame>(framesCount);
        for (int index = 0; index < framesCount; index++)
        {
            _frames.Add(new Frame(0, ChannelsCount));
        }
        WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(FrameRate.Value, ChannelsCount);
    }

    public Wave(Wave wave)
    {
        ChannelsCount = wave.ChannelsCount;
        FrameRate = wave.FrameRate;
        var framesCount = wave.FramesCount;
        _frames = new List<Frame>(framesCount);
        foreach (var frame in wave)
            _frames.Add((Frame)wave[frame].Clone());
        WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(FrameRate.Value, ChannelsCount);
    }
}
