using Acetza.Muza.WaveNS;

namespace Acetza.Muza.Wavers;

public interface Waver
{
    public Wave Generate();
    public double Frequency { get; set; }
    public double Amplitude { get; set; }
}
