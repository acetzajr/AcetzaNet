namespace Acetza.Muza.WaveNS;

public partial class Wave
{
    public int Channels { get; }
    public FrameRate FrameRate { get; }
    public Frame this[int frame]
    {
        get => _frames[frame];
        set => _frames[frame] = value;
    }
    public double Duration
    {
        get => FrameRate.IndexToTime(FramesCount);
    }
    public int FramesCount
    {
        get => _frames.Count;
        set
        {
            if (FramesCount == value)
                return;
            if (FramesCount < value)
            {
                int additions = value - FramesCount;
                for (int i = 0; i < additions; i++)
                    _frames.Add(new Frame(0, Channels));
                return;
            }
            int elements = FramesCount - value;
            for (int i = 0; i < elements; i++)
                _frames.RemoveAt(FramesCount - 1);
        }
    }
    public double Max
    {
        get
        {
            double max = 0;
            foreach (var sample in Samples)
                if (sample > max)
                    max = sample;
            return max;
        }
    }
}
