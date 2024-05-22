using Muza.Preprocess.Interfaces;
using Muza.Preprocess.Interfaces.Amplitude;
using Muza.Preprocess.Interfaces.Duration;
using Muza.Preprocess.Interfaces.Frequency;

namespace Muza.Preprocess.Blocks;

public interface IBlock : IWaver, IFrequency, IAmplitude, IDuration { }
