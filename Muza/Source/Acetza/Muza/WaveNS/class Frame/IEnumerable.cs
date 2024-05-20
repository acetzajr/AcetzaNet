using System.Collections;

namespace Acetza.Muza.WaveNS;

public partial class Frame : IEnumerable<IndexedValue<double>>
{
    public IEnumerator<IndexedValue<double>> GetEnumerator()
    {
        for (int channel = 0; channel < ChannelsCount; channel++)
        {
            yield return new(channel, this[channel]);
        }
        yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
