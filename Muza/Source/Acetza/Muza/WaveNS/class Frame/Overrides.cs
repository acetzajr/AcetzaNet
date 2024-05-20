using System.Text;

namespace Acetza.Muza.WaveNS;

public partial class Frame : IEnumerable<IndexedValue<double>>
{
    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append('[');
        foreach (var iv in this)
        {
            sb.Append($"({iv.Value:n9})");
        }
        sb.Append(']');
        return sb.ToString();
    }
}
