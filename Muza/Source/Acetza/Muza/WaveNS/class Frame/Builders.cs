namespace Acetza.Muza.WaveNS;

public partial class Frame : IEnumerable<IndexedValue<double>>
{
    public Frame(double fill = 0, int channels = 2)
    {
        _samples = new double[channels];
        if (fill == 0)
            return;
        for (int channel = 0; channel < ChannelsCount; channel++)
        {
            this[channel] = fill;
        }
    }
}
