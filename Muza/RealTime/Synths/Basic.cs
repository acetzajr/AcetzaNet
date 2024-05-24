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
    private readonly object _dictionaryLock = new();
    private readonly object _queueLock = new();

    public void BeginProcess(WaveBuffer.Block block)
    {
        lock (_dictionaryLock)
        {
            foreach (var kv in _dictionary)
            {
                if (kv.Value.Playing)
                {
                    ProcessNote(block, kv.Value);
                }
            }
        }
        lock (_queueLock)
        {
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
    }

    private void ProcessRelease(WaveBuffer.Block block, ReleaseState state)
    {
        //Console.WriteLine($"state.Amplitude: {state.Amplitude}");
        //Console.WriteLine($"_releaseDecrement: {_releaseDecrement}");
        for (int frame = 0; frame < block.FramesCount; frame++)
        {
            state.Amplitude -= _releaseDecrement;
            if (state.Amplitude < 0)
            {
                state.Playing = false;
                return;
            }
            var time = state.Time + Constants.FrameRate.IndexToTime(frame);
            var part = time * state.Frequency % 1.0;
            var sample = WaveForm(part) * state.Amplitude * _amplitude;
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
            var sample = WaveForm(part) * state.Amplitude * _amplitude;
            foreach (var channel in block.Channels)
            {
                block[frame, channel] += sample;
            }
        }
        state.Time += Constants.FrameRate.IndexToTime(block.FramesCount);
    }

    private static double WaveForm(double part)
    {
        return Primitives.Sqr(part) + Primitives.Saw(part) / 2;
    }

    public void EndProcess(WaveBuffer.Block block) { }

    private static int NormalizeNoteNumber(int number)
    {
        return number - 62;
    }

    public void NoteOff(string name, int number, int velocity)
    {
        PlayState state;
        lock (_dictionaryLock)
        {
            if (NormalizeNoteNumber(number) is int note)
            {
                state = _dictionary[note];
                state.Playing = false;
            }
        }
        lock (_queueLock)
        {
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
        lock (_dictionaryLock)
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
    }

    public static double Amplitude(int velocity)
    {
        return -1 / MzMath.ToDB(velocity / 127.0);
    }
}
