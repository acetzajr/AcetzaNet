using Muza.Preprocess.WaveNS;

namespace Muza.Preprocess.Blocks;

public partial class Enveloper
{
    Until UntilRelease(Wave wave)
    {
        double releaseStart = wave.Duration - Release;
        if (releaseStart <= 0)
            return new Until(0, 1);
        Result result = Transform(wave, AttackTransformer, 0, 0, Attack, 1, releaseStart);
        if (result.Disrupted)
            return new Until(releaseStart, result.Amplitude);
        double holdEnd = Attack + Hold;
        if (holdEnd >= releaseStart)
            return new Until(releaseStart, 1);
        double decayEnd = holdEnd + Decay;
        result = Transform(wave, DecayTransformer, holdEnd, 1, decayEnd, Sustain, releaseStart);
        if (result.Disrupted)
            return new Until(releaseStart, result.Amplitude);
        int start = wave.FrameRate.TimeToIndex(decayEnd);
        int end = wave.FrameRate.TimeToIndex(releaseStart);
        for (int frame = start; frame < end; frame++)
        {
            wave[frame] *= Sustain;
        }
        return new Until(releaseStart, Sustain);
    }
}
