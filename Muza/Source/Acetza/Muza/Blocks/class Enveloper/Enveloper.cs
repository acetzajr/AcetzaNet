using Acetza.Muza.Functions;
using Acetza.Muza.WaveNS;

namespace Acetza.Muza.Blocks;

public partial class Enveloper(
    IBlock? block = null,
    double attack = 1.0 / 16,
    double hold = 1.0 / 12,
    double decay = 1.0 / 8,
    double sustain = 1.0 / 2,
    double release = 1.0 / 4,
    Transformer? attackTransformer = null,
    Transformer? decayTransformer = null,
    Transformer? releaseTransformer = null
) : IBlock
{
    public IBlock Block { get; set; } = block is null ? new Basic() : block;
    public double Attack { get; set; } = attack;
    public double Hold { get; set; } = hold;
    public double Decay { get; set; } = decay;
    public double Sustain { get; set; } = sustain;
    public double Release { get; set; } = release;
    public Transformer AttackTransformer { get; set; } =
        attackTransformer is null ? Transformers.Smooth : attackTransformer;
    public Transformer DecayTransformer { get; set; } =
        decayTransformer is null ? Transformers.Smooth : decayTransformer;
    public Transformer ReleaseTransformer { get; set; } =
        releaseTransformer is null ? Transformers.Smooth : releaseTransformer;
    public double Frequency
    {
        get => Block.Frequency;
        set => Block.Frequency = value;
    }
    public double Amplitude
    {
        get => Block.Amplitude;
        set => Block.Amplitude = value;
    }
    public double Duration
    {
        get => Block.Duration;
        set => Block.Duration = value;
    }

    public Wave Wave
    {
        get
        {
            var wave = Block.Wave;
            var until = UntilRelease(wave);
            Transform(
                wave,
                ReleaseTransformer,
                until.Time,
                until.Amplitude,
                wave.Duration,
                0,
                wave.Duration
            );
            return wave;
        }
    }
}
