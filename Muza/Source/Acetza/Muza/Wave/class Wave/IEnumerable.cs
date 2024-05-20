using System.Collections;

namespace Acetza.Muza.Wave;



public partial class Wave : IEnumerable<IndexedValue<Frame>>
{
    public IEnumerator<IndexedValue<Frame>> GetEnumerator()
    {
        for (int frame = 0; frame < FramesCount; ++frame)
        {
            yield return new(frame, this[frame]);
        }
        yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}