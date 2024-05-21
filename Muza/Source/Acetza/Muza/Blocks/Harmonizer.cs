using Acetza.Muza.Functions;
using Acetza.Muza.WaveNS;

namespace Acetza.Muza.Blocks;

public class Harmonizer(
    int depth = 7,
    IBlock? block = null,
    Numberer? numberer = null,
    Amplituder? amplituder = null
) : IBlock
{
    public int Depth { get; set; } = depth;
    public IBlock Block { get; set; } = block is null ? new Basic() : block;
    public Numberer Numberer { get; set; } = numberer is null ? Numberers.Sawtooth : numberer;
    public Amplituder Amplituder { get; set; } =
        amplituder is null ? Amplituders.Standar : amplituder;
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
            var frequency = Block.Frequency;
            for (int index = 2; index <= Depth; index++)
            {
                var number = Numberer(index);
                Block.Frequency = Frequency * number;
                wave.Add(Block.Wave, amplitude: Amplituder(index, number, Depth));
            }
            Block.Frequency = frequency;
            wave.Normalize();
            wave *= Amplitude;
            return wave;
        }
    }
}
