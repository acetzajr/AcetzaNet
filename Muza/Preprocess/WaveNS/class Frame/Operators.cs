namespace Muza.Preprocess.WaveNS;

public partial class Frame
{
    public static Frame operator *(Frame frame, double amplitude)
    {
        foreach (var channel in frame)
            frame[channel] *= amplitude;
        return frame;
    }

    public static Frame operator /(Frame frame, double amplitude)
    {
        foreach (var channel in frame)
            frame[channel] /= amplitude;
        return frame;
    }

    public static Frame operator +(Frame a, Frame b)
    {
        foreach (var channel in a)
            a[channel] += b[channel];
        return a;
    }
}
