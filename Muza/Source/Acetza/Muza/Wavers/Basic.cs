using Acetza.Muza.Functions;
using Acetza.Muza.WaveNS;

namespace Acetza.Muza.Wavers;

public class Basic(
    Primitive? primitive = null,
    double frequency = 360,
    double duration = 1,
    double amplitude = 1,
    int frameRate = 44_100
)
{
    public Primitive Primitive { get; set; } = primitive is null ? Primitives.Sin : primitive;
    public double Frequency { get; set; } = frequency;
    public double Duration { get; set; } = duration;
    public double Amplitude { get; set; } = amplitude;
    public int FrameRate { get; set; } = frameRate;

    public Wave Generate()
    {
        var wave = new Wave(Duration, FrameRate);
        foreach (var iv in wave)
        {
            var time = wave.FrameRate.IndexToTime(iv.Index);
            var part = time * Frequency % 1.0;
            var sample = Primitive(part) * Amplitude;
            wave[iv.Index] = new(sample);
        }
        return wave;
    }
}