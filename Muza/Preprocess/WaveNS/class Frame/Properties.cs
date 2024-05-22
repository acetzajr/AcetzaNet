namespace Muza.Preprocess.WaveNS;

public partial class Frame
{
    public int ChannelsCount
    {
        get { return _samples.Length; }
    }
    public double this[int channel]
    {
        get { return _samples[channel]; }
        set { _samples[channel] = value; }
    }
}
