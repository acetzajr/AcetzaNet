namespace Muza.RealTime;

public class Session : IDisposable
{
    public Session()
    {
        _midiManager = new MidiManager();
    }

    public void Start()
    {
        Console.WriteLine("Press enter to exit...");
        _midiManager.Start();
        Console.ReadLine();
        _midiManager.Stop();
    }

    private readonly MidiManager _midiManager;

    // Dispose pattern
    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                _midiManager.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~Session()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
