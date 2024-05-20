namespace Acetza.Muza.Wave;

public partial class Wave
{
    public Wave(double duration, int frameRate = 44_100)
    {
        FrameRate = new FrameRate(frameRate);
        var framesCount = FrameRate.TimeToIndex(duration);
        _frames = new List<Frame>(framesCount);
        for (int index = 0; index < framesCount; index++)
        {
            _frames.Add(new Frame());
        }
    }
}