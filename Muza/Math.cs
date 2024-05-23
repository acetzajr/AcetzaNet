namespace Muza;

public static class MzMath
{
    public static int PMod(int n, int mod)
    {
        return n < 0 ? (n % mod + mod) % mod : n % mod;
    }

    public static double ToDB(double amplitude)
    {
        return 10 * Math.Log10(amplitude);
    }

    public static double FromDB(double db)
    {
        return Math.Pow(10, db / 10);
    }
}
