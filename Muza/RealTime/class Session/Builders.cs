using NAudio.Wave;

namespace Muza.RealTime;

public partial class Session
{
    public Session()
    {
        _midiManager = new MidiManager();
        WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(
            Constants.FrameRate.Value,
            Constants.Channels
        );
        _asioThread = new Thread(PlayAsio);
        _playThread = new Thread(Play);
        _synths = [];
    }
}
