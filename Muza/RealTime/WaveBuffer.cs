namespace Muza.RealTime;

public class WaveBuffer
{
    public delegate void BlockEventHandler(Block block);
    public event BlockEventHandler? BlockRequested;

    public class Block(int frames = 128, int channels = 2)
    {
        public bool Ready { get; set; } = true;
        public int FramesCount { get; } = frames;
        public int ChannelsCount { get; } = channels;
        public int SamplesCount
        {
            get => _samples.Length;
        }
        public double this[int index]
        {
            get => _samples[index];
            set => _samples[index] = value;
        }
        public double this[int frame, int channel]
        {
            get => _samples[frame * ChannelsCount + channel];
            set => _samples[frame * ChannelsCount + channel] = value;
        }

        public void Empty()
        {
            for (int i = 0; i < SamplesCount; i++)
            {
                _samples[i] = 0;
            }
        }

        public IEnumerable<int> Channels
        {
            get
            {
                for (int channel = 0; channel < ChannelsCount; channel++)
                {
                    yield return channel;
                }
                yield break;
            }
        }
        public IEnumerable<int> Frames
        {
            get
            {
                for (int frame = 0; frame < FramesCount; frame++)
                {
                    yield return frame;
                }
                yield break;
            }
        }
        public IEnumerable<int> Samples
        {
            get
            {
                for (int sample = 0; sample < SamplesCount; sample++)
                {
                    yield return sample;
                }
                yield break;
            }
        }
        readonly double[] _samples = new double[frames * channels];
    }

    public WaveBuffer(int blocks = 2, int frames = 128, int channels = 2)
    {
        Current = new Block(frames, channels);
        _back = new Block[blocks];
        for (int i = 0; i < blocks; i++)
        {
            _back[i] = new Block(frames, channels);
        }
    }

    public int BlocksCount
    {
        get => _back.Length;
    }
    public Block Current { get; set; }
    public Block Last
    {
        get => _back[^1];
    }
    public Block First
    {
        get => _back[0];
        set => _back[0] = value;
    }

    public double NextSample()
    {
        if (_sampleIndex >= Current.SamplesCount)
        {
            if (TryAdvance())
                return Current[_sampleIndex++];
            //Console.WriteLine("in need");
            return 0;
        }
        return Current[_sampleIndex++];
    }

    private bool TryAdvance()
    {
        if (!_back[^1].Ready)
            return false;
        _sampleIndex = 0;
        Block swap = Current;
        swap.Empty();
        swap.Ready = false;
        BlockRequested?.Invoke(swap);
        Current = Last;
        for (int i = BlocksCount - 2; i >= 0; i--)
        {
            _back[i + 1] = _back[i];
        }
        First = swap;
        return true;
    }

    private int _sampleIndex = 0;
    private readonly Block[] _back;
}
