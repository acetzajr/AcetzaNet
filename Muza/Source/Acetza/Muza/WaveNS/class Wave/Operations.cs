namespace Acetza.Muza.WaveNS;

public partial class Wave
{
    public Wave Add(Wave wave, double time = 0)
    {
        int start = FrameRate.TimeToIndex(time);
        int end = start + wave.FramesCount;
        if (end > FramesCount)
            FramesCount = end;
        for (int i = start, j = 0; i < end; i++, j++)
        {
            this[i] += wave[j];
        }
        return this;
    }

    public Wave Normalize()
    {
        var max = Max;
        if (max == 0)
            return this;
        return this / max;
    }
}
