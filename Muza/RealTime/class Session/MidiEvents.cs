using Muza.RealTime.Interfaces;

namespace Muza.RealTime;

public partial class Session : IMidiHandler
{
    public void NoteOn(string name, int number, int velocity)
    {
        Console.Write("> Note on -> ");
        Console.Write($"name: {name}");
        Console.Write($", number: {number}");
        Console.Write($", velocity: {velocity}");
        Console.WriteLine();
    }

    public void NoteOff(string name, int number, int velocity)
    {
        Console.Write("> Note off -> ");
        Console.Write($"name: {name}");
        Console.Write($", number: {number}");
        Console.Write($", velocity: {velocity}");
        Console.WriteLine();
    }
}
