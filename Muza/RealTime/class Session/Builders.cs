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
        playThread = new Thread(Play);
        _synths = [];
        _latency = 40;
        _waveBuffer = new WaveBuffer(2, 360);
        _waveBuffer.BlockRequested += BlockEventHandler;
        _mutex = new Mutex();
    }
}
