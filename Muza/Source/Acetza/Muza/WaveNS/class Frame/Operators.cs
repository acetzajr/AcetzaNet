namespace Acetza.Muza.WaveNS;

public partial class Frame
{
    public static Frame operator *(Frame frame, double amplitude)
    {
        foreach (var iv in frame)
        {
            frame[iv.Index] *= amplitude;
        }
        return frame;
    }
}
