namespace Muza.Notes;

public static class EqualTempered
{
    public static double Frequency(int note, double baseFrequency = 440)
    {
        return baseFrequency * Math.Pow(2, note / 12.0);
    }
}
