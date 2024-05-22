using static Muza.RealTime.WaveBuffer;

namespace Muza.RealTime.Interfaces;

public interface IBlockHandler
{
    public void BlockEventHandler(Block block);
}
