namespace Acetza.Muza.WaveNS;

public partial class Wave
{
    public Wave(double duration, int channels = 2, int frameRate = 44_100)
    {
        Channels = channels;
        FrameRate = new FrameRate(frameRate);
        var framesCount = FrameRate.TimeToIndex(duration);
        _frames = new List<Frame>(framesCount);
        for (int index = 0; index < framesCount; index++)
        {
            _frames.Add(new Frame(0, Channels));
        }
    }

    public Wave(Wave wave)
    {
        Channels = wave.Channels;
        FrameRate = wave.FrameRate;
        var framesCount = wave.FramesCount;
        _frames = new List<Frame>(framesCount);
        foreach (var frame in wave)
            _frames.Add((Frame)wave[frame].Clone());
    }
}
