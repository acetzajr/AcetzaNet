using Acetza.Muza.Functions;
using Acetza.Muza.WaveNS;

namespace Acetza.Muza.Wavers;

public class Harmonizer(
    int depth = 7,
    Waver? fundamental = null,
    Waver? harmonics = null,
    Numberer? numberer = null,
    Amplituder? amplituder = null
) : Waver
{
    public int Depth { get; set; } = depth;
    public Waver Fundamental { get; set; } = fundamental is null ? new Basic() : fundamental;
    public Waver Harmonics { get; set; } = harmonics is null ? new Basic() : harmonics;
    public Numberer Numberer { get; set; } = numberer is null ? Numberers.Sawtooth : numberer;
    public Amplituder Amplituder { get; set; } =
        amplituder is null ? Amplituders.Standar : amplituder;
    public double Frequency
    {
        get => Fundamental.Frequency;
        set => Fundamental.Frequency = value;
    }
    public double Amplitude
    {
        get => Fundamental.Amplitude;
        set => Fundamental.Amplitude = value;
    }

    public Wave Generate()
    {
        var wave = Fundamental.Generate();
        for (int index = 2; index <= Depth; index++)
        {
            var number = Numberer(index);
            Harmonics.Frequency = Frequency * number;
            Harmonics.Amplitude = Amplituder(index, number, Depth);
            wave.Add(Harmonics.Generate());
        }
        wave.Normalize();
        wave *= Amplitude;
        return wave;
    }
}
