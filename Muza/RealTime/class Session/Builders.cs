using NAudio.Wave;

namespace Muza.RealTime;

public partial class Session
{
    public Session()
    {
        _midiManager = new MidiManager();
        _midiManager.NoteOn += NoteOn;
        _midiManager.NoteOff += NoteOff;
        WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(Constants.FrameRate, Constants.Channels);
        playThread = new Thread(Play);
        _synths = [];
        _waveBuffer = new WaveBuffer();
        _waveBuffer.BlockRequested += BlockEventHandler;
        _blockProcessingThread = new Thread(ProcessBlocks);
    }
}
