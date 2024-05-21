using Acetza.Muza.Notes;

namespace Acetza.Muzer;

static partial class Tests
{
    public static void Test1()
    {
        var scale = Scale.Acetza();
        int limits = 23;
        for (int index = -limits; index < 0; index++)
        {
            Console.WriteLine($"> Power: {scale.Power(index)}, Note: {scale.Note(index)}");
        }
        Console.WriteLine("->   ->");
        for (int index = 0; index < limits; index++)
        {
            Console.WriteLine($"> Power: {scale.Power(index)}, Note: {scale.Note(index)}");
        }
    }
}
