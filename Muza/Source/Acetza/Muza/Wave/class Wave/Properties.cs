namespace Acetza.Muza.Wave;

public partial class Wave
{
    public FrameRate FrameRate { get; }
    public int FramesCount { get { return _frames.Count; } }
    public Frame this[int frame]
    {
        get { return _frames[frame]; }
        set { _frames[frame] = value; }
    }
}