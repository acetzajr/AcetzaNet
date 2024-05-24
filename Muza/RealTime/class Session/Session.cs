namespace Muza.RealTime;

public partial class Session
{
    public void Start()
    {
        if (_useAsio)
        {
            if (!StartPlayingAsio())
                return;
        }
        else
        {
            StartPlaying();
        }
        Console.WriteLine("Press enter to exit...");
        _playing = true;
        _midiManager.Start();
        Console.ReadLine();
        _playing = false;
        _midiManager.Stop();
        if (_useAsio)
        {
            StopPlayingAsio();
        }
        else
        {
            StopPlaying();
        }
    }
}
