using Muza.RealTime.Interfaces;

namespace Muza.RealTime;

public partial class Session
{
    public void AddSynth(ISynth synth)
    {
        _synths.Add(synth);
        _midiManager.NoteOn += synth.NoteOn;
        _midiManager.NoteOff += synth.NoteOff;
    }
}
