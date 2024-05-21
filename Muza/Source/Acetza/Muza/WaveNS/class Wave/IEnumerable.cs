using System.Collections;

namespace Acetza.Muza.WaveNS;

public partial class Wave : IEnumerable<int>
{
    public IEnumerator<int> GetEnumerator()
    {
        for (int frame = 0; frame < FramesCount; ++frame)
        {
            yield return frame;
        }
        yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<double> Samples
    {
        get
        {
            foreach (var frame in this)
            {
                foreach (var channel in this[frame])
                {
                    yield return this[frame][channel];
                }
            }
            yield break;
        }
    }
}
