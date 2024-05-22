using System.Collections;

namespace Muza.Preprocess.WaveNS;

public partial class Frame : IEnumerable<int>
{
    public IEnumerator<int> GetEnumerator()
    {
        for (int channel = 0; channel < ChannelsCount; channel++)
        {
            yield return channel;
        }
        yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
