namespace Muza.Preprocess.WaveNS;

public partial class Frame : ICloneable
{
    public object Clone()
    {
        return new Frame(this);
    }
}
