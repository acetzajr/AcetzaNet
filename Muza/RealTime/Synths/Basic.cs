using System.Collections.Concurrent;
using Muza.Notes;
using Muza.Preprocess.Functions;
using Muza.RealTime.Interfaces;

namespace Muza.RealTime.Synths;

public class Basic : ISynth
{
    private class PlayState(bool playing = true, double time = 0)
    {
        public bool Playing { get; set; } = playing;
        public double Time { get; set; } = time;

        public void Reset()
        {
            Playing = true;
            Time = 0;
        }
    }

    private readonly Scale _scale = Scale.Acetza();
    private readonly ConcurrentDictionary<int, PlayState> _dictionary = new();

    public void BeginProcess(WaveBuffer.Block block)
    {
        foreach (var kv in _dictionary)
        {
            if (kv.Value.Playing)
            {
                ProcessNote(block, kv.Key, kv.Value);
            }
        }
    }

    private void ProcessNote(WaveBuffer.Block block, int note, PlayState state)
    {
        var frequency = _scale.Frequency(note);
        foreach (var frame in block.Frames)
        {
            var time = state.Time + Constants.FrameRate.IndexToTime(frame);
            var part = time * frequency % 1.0;
            var sample = Primitives.Tri(part) * 1.0 / 8.0;
            foreach (var channel in block.Channels)
            {
                block[frame, channel] += sample;
            }
        }
        state.Time += Constants.FrameRate.IndexToTime(block.FramesCount);
    }

    public void EndProcess(WaveBuffer.Block block) { }

    private static int? NormalizeNoteNumber(int number)
    {
        number -= 62;
        if (MzMath.PMod(number, 12) == 6)
        {
            return null;
        }
        var offsets = Math.Abs(number / 6);
        offsets = offsets % 2 == 1 ? (offsets + 1) / 2 : offsets / 2;
        number = number < 0 ? number + offsets : number - offsets;
        Console.WriteLine($"note: {number}");
        return number;
    }

    public void NoteOff(string name, int number, int velocity)
    {
        if (NormalizeNoteNumber(number) is int note)
        {
            _dictionary[note].Playing = false;
        }
    }

    public void NoteOn(string name, int number, int velocity)
    {
        if (NormalizeNoteNumber(number) is int note)
        {
            if (_dictionary.TryGetValue(note, out PlayState? value))
            {
                value.Reset();
            }
            else
            {
                _dictionary[note] = new PlayState();
            }
        }
    }
}
