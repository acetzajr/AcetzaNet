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
}
