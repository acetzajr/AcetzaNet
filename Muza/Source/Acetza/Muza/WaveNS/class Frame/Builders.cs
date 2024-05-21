namespace Acetza.Muza.WaveNS;

public partial class Frame
{
    public Frame(double fill = 0, int channels = 2)
    {
        _samples = new double[channels];
        if (fill == 0)
            return;
        foreach (var channel in this)
            this[channel] = fill;
    }
}
