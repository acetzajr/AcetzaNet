using Muza.RealTime.Interfaces;
using NAudio.Wave;

namespace Muza.RealTime;

public partial class Session
{
    private readonly bool _useAsio = true;
    private Thread _asioThread;
    private Thread _playThread;
    private bool _playing = false;
    private readonly MidiManager _midiManager;
    private readonly List<ISynth> _synths;
    private AsioOut? _asio;
    private WaveOutEvent? _waveOut;
}
