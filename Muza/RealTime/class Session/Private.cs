using Muza.RealTime.Interfaces;
using NAudio.Wave;

namespace Muza.RealTime;

public partial class Session
{
    private void StartPlaying()
    {
        playThread.Start();
    }

    private void StopPlaying()
    {
        playThread.Join();
    }

    private void Play()
    {
        Console.WriteLine("Playing");
        using var outputDevice = new WasapiOut();
        outputDevice.Init(this);
        outputDevice.Play();
        while (outputDevice.PlaybackState == PlaybackState.Playing)
        {
            Thread.Sleep(500);
        }
    }

    private int _latency;
    private Thread playThread;
    private bool _playing = false;
    private readonly MidiManager _midiManager;
    private readonly List<ISynth> _synths;
}
