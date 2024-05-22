namespace Muza.RealTime;

public partial class Session
{
    public void Start()
    {
        Console.WriteLine("Press enter to exit...");
        _playing = true;
        StartBlockProcessing();
        StartPlaying();
        _midiManager.Start();
        Console.ReadLine();
        _playing = false;
        _midiManager.Stop();
        StopPlaying();
        EndBlockProcessing();
    }
}
