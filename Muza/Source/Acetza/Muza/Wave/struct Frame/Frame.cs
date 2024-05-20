namespace Acetza.Muza.Wave;
public struct Frame(double left, double right)
{
    public Frame() : this(0.0, 0.0) { }
    public double Left { get; set; } = left;
    public double Right { get; set; } = right;
    public double this[int channel]
    {
        get
        {
            if (channel == 0) return Left;
            if (channel == 1) return Right;
            throw new ArgumentException($"channel must be 0 (Left) or 1 (Right), {channel} was provided");
        }
        set
        {
            if (channel == 0) Left = value;
            if (channel == 1) Right = value;
            throw new ArgumentException($"channel must be 0 (Left) or 1 (Right), {channel} was provided");
        }
    }
    public override string ToString()
    {
        return $"({Left:n9},{Right:n9})";
    }
}