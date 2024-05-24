using NAudio.Wave;

namespace Muza.RealTime;

public partial class Session
{
    private void StartPlaying()
    {
        _waveOut = new WaveOutEvent();
        _waveOut.DesiredLatency = 64;
        _waveBuffer = new WaveBuffer(
            2,
            (int)Math.Ceiling(_waveOut.DesiredLatency * Constants.FrameRate.Value / 1000.0)
        );
        _waveBuffer.BlockRequested += BlockEventHandler;
        _waveOut.Init(this);
        _waveOut.Play();
        _playThread.Start();
    }

    private void Play()
    {
        while (_waveOut?.PlaybackState == PlaybackState.Playing)
        {
            Thread.Sleep(1000);
        }
    }

    private void StopPlaying()
    {
        _waveOut?.Stop();
        _playThread.Join();
    }

    private bool StartPlayingAsio()
    {
        var driver = ChooseDriver();
        _asio = new AsioOut(driver);
        if (_asio is null)
        {
            Console.WriteLine("Could not init asio");
            return false;
        }
        Console.WriteLine($"PlaybackLatency: {_asio.PlaybackLatency}");
        _waveBuffer = new WaveBuffer(2, _asio.PlaybackLatency);
        _waveBuffer.BlockRequested += BlockEventHandler;
        _asio.Init(this);
        _asio.Play();
        _asioThread.Start();
        return true;
    }

    private void StopPlayingAsio()
    {
        _asio?.Stop();
        _asioThread.Join();
    }

    public static string ChooseDriver()
    {
        List<string> drivers = [];
        foreach (var name in AsioOut.GetDriverNames())
        {
            drivers.Add(name);
        }
        while (true)
        {
            Console.WriteLine("> Asio drivers:");
            for (int i = 0; i < drivers.Count; i++)
            {
                Console.WriteLine($"{i}: {drivers[i]}");
            }
            Console.Write("Choose one driver by number: ");
            string? line = Console.ReadLine();
            string input = line is null ? "" : line;
            bool result = int.TryParse(input, out int driverIndex);
            if (!result)
            {
                Console.WriteLine("Input must be a number, try again");
                continue;
            }
            if (driverIndex >= drivers.Count || driverIndex < 0)
            {
                Console.WriteLine($"Input must be in range [0 -> {drivers.Count - 1}], try again");
                continue;
            }
            return drivers[driverIndex];
        }
    }

    private void PlayAsio()
    {
        while (_asio?.PlaybackState == PlaybackState.Playing)
        {
            Thread.Sleep(500);
        }
    }
}
