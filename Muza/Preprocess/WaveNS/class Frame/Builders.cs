namespace Muza.Preprocess.WaveNS;

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

    public Frame(Frame frame)
    {
        _samples = new double[frame.ChannelsCount];
        foreach (var channel in frame)
            _samples[channel] = frame[channel];
    }
}
