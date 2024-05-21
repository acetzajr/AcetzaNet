using Acetza.Muza.Blocks;
using Acetza.Muza.Functions;
using Acetza.Muza.WaveNS;

namespace Acetza.Muza.Synths;

public class Synth1 : IWaver
{
    public Synth1()
    {
        _block = new Enveloper(block: new Harmonizer(block: new Basic(primitive: Primitives.Tri)));
    }

    private readonly Enveloper _block;

    public Wave Wave
    {
        get
        {
            var wave = _block.Wave;
            return wave;
        }
    }
}
