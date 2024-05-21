using Acetza.Muza.Interfaces;
using Acetza.Muza.Interfaces.Amplitude;
using Acetza.Muza.Interfaces.Duration;
using Acetza.Muza.Interfaces.Frequency;

namespace Acetza.Muza.Blocks;

public interface IBlock : IWaver, IFrequency, IAmplitude, IDuration { }
