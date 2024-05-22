using System.Collections.Concurrent;
using Muza.RealTime.Interfaces;

namespace Muza.RealTime;

public partial class Session : IBlockHandler
{
    public void BlockEventHandler(WaveBuffer.Block block)
    {
        _blocks.Enqueue(block);
    }

    void StartBlockProcessing()
    {
        _blockProcessingThread.Start();
    }

    void EndBlockProcessing()
    {
        _blockProcessingThread.Join();
    }

    void ProcessBlocks()
    {
        WaveBuffer.Block? block;
        while (_playing)
        {
            if (_blocks.TryDequeue(out block))
            {
                foreach (var synth in _synths)
                {
                    synth.BeginProcess(block);
                }
                foreach (var synth in _synths)
                {
                    synth.EndProcess(block);
                }
            }
        }
    }

    private readonly Thread _blockProcessingThread;
    private readonly WaveBuffer _waveBuffer;
    private readonly ConcurrentQueue<WaveBuffer.Block> _blocks = new();
}
