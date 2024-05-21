namespace Acetza.Muza.Blocks;

public partial class Enveloper
{
    readonly struct Until(double time, double amplitude)
    {
        public double Time { get; } = time;
        public double Amplitude { get; } = amplitude;
    }

    struct Result(bool disrupted, double time, double amplitude)
    {
        public bool Disrupted { get; } = disrupted;
        public double Time { get; } = time;
        public double Amplitude { get; } = amplitude;
    }
}
