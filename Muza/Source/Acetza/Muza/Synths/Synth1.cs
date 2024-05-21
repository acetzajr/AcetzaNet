using Acetza.Muza.Blocks;
using Acetza.Muza.Functions;
using Acetza.Muza.WaveNS;

namespace Acetza.Muza.Synths;

public class Synth1 : IBlock
{
    public Synth1()
    {
        _block = new Enveloper(
            block: new Harmonizer(block: new Basic(primitive: Primitives.Sqr)),
            attack: .025,
            hold: .1,
            release: .1
        );
    }

    private readonly Enveloper _block;

    public Wave Wave
    {
        get => _block.Wave;
    }

    public double Frequency
    {
        get => _block.Frequency;
        set => _block.Frequency = value;
    }
    public double Amplitude
    {
        get => _block.Amplitude;
        set => _block.Amplitude = value;
    }
    public double Duration
    {
        get => _block.Duration;
        set => _block.Duration = value;
    }
}
