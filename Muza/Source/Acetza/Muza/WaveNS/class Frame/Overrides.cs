using System.Text;

namespace Acetza.Muza.WaveNS;

public partial class Frame
{
    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append('[');
        foreach (var channel in this)
        {
            sb.Append($"({this[channel]:n9})");
        }
        sb.Append(']');
        return sb.ToString();
    }
}
