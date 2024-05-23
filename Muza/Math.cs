namespace Muza;

public static class MzMath
{
    public static int PMod(int n, int mod)
    {
        return n < 0 ? (n % mod + mod) % mod : n % mod;
    }
}
