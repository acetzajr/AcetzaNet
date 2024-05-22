namespace Muza.Preprocess.WaveNS;

public partial class Wave : ICloneable
{
    public Wave Add(Wave wave, double time = 0, double amplitude = 1)
    {
        int start = FrameRate.TimeToIndex(time);
        int end = start + wave.FramesCount;
        if (end > FramesCount)
            FramesCount = end;
        for (int i = start, j = 0; i < end; i++, j++)
        {
            this[i] += wave[j] * amplitude;
        }
        return this;
    }

    public Wave Mul(double amplitude)
    {
        return this * amplitude;
    }

    public object Clone()
    {
        return new Wave(this);
    }

    public Wave Normalize()
    {
        var max = Max;
        if (max == 0)
            return this;
        return this / max;
    }

    public Wave Reverb(int iterations = 7, double delay = 1.0 / 12, double decay = 1.0 / 2.0)
    {
        var clone = (Wave)this.Clone();
        var time = delay;
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            Add(clone, time, decay);
            time += delay;
            delay /= 2;
            decay /= 2;
        }
        return this;
    }
}
