using Acetza.Muza.Functions;
using Acetza.Muza.WaveNS;

namespace Acetza.Muza.Blocks;

public class Basic(
    Primitive? primitive = null,
    double frequency = 360,
    double duration = 1,
    double amplitude = 1,
    int channels = 2,
    int frameRate = 44_100
) : IBlock
{
    public Primitive Primitive { get; set; } = primitive is null ? Primitives.Sin : primitive;
    public double Frequency { get; set; } = frequency;
    public double Duration { get; set; } = duration;
    public double Amplitude { get; set; } = amplitude;
    public int Channels { get; set; } = channels;
    public int FrameRate { get; set; } = frameRate;

    public Wave Wave
    {
        get
        {
            var wave = new Wave(Duration, Channels, FrameRate);
            foreach (var frame in wave)
            {
                var time = wave.FrameRate.IndexToTime(frame);
                var part = time * Frequency % 1.0;
                var sample = Primitive(part) * Amplitude;
                wave[frame] = new(sample);
            }
            return wave;
        }
    }
}
