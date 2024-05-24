using System.Collections.Concurrent;
using Muza.RealTime.Interfaces;

namespace Muza.RealTime;

public partial class Session : IBlockHandler
{
    public void BlockEventHandler(WaveBuffer.Block block)
    {
        _blocks.Enqueue(block);
        ThreadPool.QueueUserWorkItem(ProcessBlocks);
    }

    void ProcessBlocks(object? state)
    {
        lock (_blocksLock)
        {
            bool working = true;
            while (working)
            {
                if (_blocks.TryDequeue(out WaveBuffer.Block? block))
                {
                    foreach (var synth in _synths)
                    {
                        synth.BeginProcess(block);
                    }
                    foreach (var synth in _synths)
                    {
                        synth.EndProcess(block);
                    }
                    block.Ready = true;
                    working = false;
                }
            }
        }
    }

    private WaveBuffer? _waveBuffer;
    private readonly ConcurrentQueue<WaveBuffer.Block> _blocks = new();
    private readonly object _blocksLock = new();
}
