using Muza.Notes;
using Muza.Preprocess.Functions;
using Muza.RealTime.Interfaces;

namespace Muza.RealTime.Synths;

public class Basic : ISynth
{
    private class PlayState(
        double frequency,
        double amplitude,
        bool playing = true,
        double time = 0
    )
    {
        public bool Playing { get; set; } = playing;
        public double Time { get; set; } = time;
        public double Amplitude { get; set; } = amplitude;
        public double Frequency { get; set; } = frequency;

        public void NoteOn(double amplitude)
        {
            Amplitude = amplitude;
            Playing = true;
            Time = 0;
        }
    }

    public class ReleaseState(double amplitude, double frequency, double time)
    {
        public double Time { get; set; } = time;
        public double Amplitude { get; set; } = amplitude;
        public double Frequency { get; set; } = frequency;
        public bool Playing { get; set; } = true;
    }

    private readonly Scale _scale = Scale.Acetza();
    private readonly Dictionary<int, PlayState> _dictionary = [];
    private readonly double _amplitude = 1.0 / 8;
    private readonly double _releaseDecrement = MzMath.FromDB(-45);
    private Queue<ReleaseState> _releasingQueue = [];
    private Queue<ReleaseState> _releasingSwap = [];

    public void BeginProcess(WaveBuffer.Block block)
    {
        foreach (var kv in _dictionary)
        {
            if (kv.Value.Playing)
            {
                ProcessNote(block, kv.Value);
            }
        }
        while (_releasingQueue.TryDequeue(out ReleaseState? state))
        {
            if (state.Playing)
            {
                ProcessRelease(block, state);
                if (state.Playing)
                    _releasingSwap.Enqueue(state);
            }
        }
        (_releasingQueue, _releasingSwap) = (_releasingSwap, _releasingQueue);
    }

    private void ProcessRelease(WaveBuffer.Block block, ReleaseState state)
    {
        //Console.WriteLine($"state.Amplitude: {state.Amplitude}");
        //Console.WriteLine($"_releaseDecrement: {_releaseDecrement}");
        foreach (var frame in block.Frames)
        {
            state.Amplitude -= _releaseDecrement;
            if (state.Amplitude < 0)
            {
                state.Playing = false;
                return;
            }
            var time = state.Time + Constants.FrameRate.IndexToTime(frame);
            var part = time * state.Frequency % 1.0;
            var sample = Primitives.Tri(part) * state.Amplitude * _amplitude;
            foreach (var channel in block.Channels)
            {
                block[frame, channel] += sample;
            }
        }
        state.Time += Constants.FrameRate.IndexToTime(block.FramesCount);
    }

    private void ProcessNote(WaveBuffer.Block block, PlayState state)
    {
        foreach (var frame in block.Frames)
        {
            var time = state.Time + Constants.FrameRate.IndexToTime(frame);
            var part = time * state.Frequency % 1.0;
            var sample = Primitives.Tri(part) * state.Amplitude * _amplitude;
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
            return null;
        var offsets = Math.Abs(number / 6);
        offsets = offsets % 2 == 1 ? (offsets + 1) / 2 : offsets / 2;
        return number < 0 ? number + offsets : number - offsets;
    }

    public void NoteOff(string name, int number, int velocity)
    {
        if (NormalizeNoteNumber(number) is int note)
        {
            var state = _dictionary[note];
            state.Playing = false;
            _releasingQueue.Enqueue(
                new ReleaseState(
                    amplitude: state.Amplitude,
                    frequency: state.Frequency,
                    time: state.Time
                )
            );
        }
    }

    public void NoteOn(string name, int number, int velocity)
    {
        if (NormalizeNoteNumber(number) is int note)
        {
            if (_dictionary.TryGetValue(note, out PlayState? value))
            {
                value.NoteOn(Amplitude(velocity));
            }
            else
            {
                _dictionary[note] = new PlayState(
                    frequency: _scale.Frequency(note),
                    amplitude: Amplitude(velocity)
                );
            }
        }
    }

    public static double Amplitude(int velocity)
    {
        return -1 / MzMath.ToDB(velocity / 127.0);
    }
}
