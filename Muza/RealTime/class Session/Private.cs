using Muza.RealTime.Interfaces;
using NAudio.Wave;

namespace Muza.RealTime;

public partial class Session
{
    private Thread playThread;
    private bool _playing = false;
    private readonly MidiManager _midiManager;
    private readonly List<ISynth> _synths;
    private AsioOut? _asio;
}
