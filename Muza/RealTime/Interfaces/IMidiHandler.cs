namespace Muza.RealTime.Interfaces;

public interface IMidiHandler
{
    public void NoteOn(string name, int number, int velocity);
    public void NoteOff(string name, int number, int velocity);
}
