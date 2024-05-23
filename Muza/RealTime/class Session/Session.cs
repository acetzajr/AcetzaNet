namespace Muza.RealTime;

public partial class Session
{
    public void Start()
    {
        if (!StartPlaying())
            return;
        Console.WriteLine("Press enter to exit...");
        _playing = true;
        _midiManager.Start();
        Console.ReadLine();
        _playing = false;
        _midiManager.Stop();
        StopPlaying();
    }
}
