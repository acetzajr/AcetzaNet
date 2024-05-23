using NAudio.Midi;

namespace Muza.RealTime;

public class MidiManager : IDisposable
{
    public delegate void NoteEventHandler(string name, int number, int velocity);
    public event NoteEventHandler? NoteOn;
    public event NoteEventHandler? NoteOff;

    public MidiManager()
    {
        _deviceNumber = ChooseDevice();
        _midiIn = new MidiIn(_deviceNumber);
        _midiIn.MessageReceived += MessageReceived;
        _midiIn.ErrorReceived += ErrorReceived;
    }

    public void Start()
    {
        _midiIn.Start();
    }

    public void Stop()
    {
        _midiIn.Stop();
    }

    void ErrorReceived(object? sender, MidiInMessageEventArgs message)
    {
        Console.WriteLine(
            string.Format(
                "Time {0} Message 0x{1:X8} Event {2}",
                message.Timestamp,
                message.RawMessage,
                message.MidiEvent
            )
        );
    }

    void MessageReceived(object? sender, MidiInMessageEventArgs message)
    {
        if (MidiEvent.IsNoteOn(message.MidiEvent))
        {
            var ev = (NoteEvent)message.MidiEvent;
            NoteOn?.Invoke(ev.NoteName, ev.NoteNumber, ev.Velocity);
            return;
        }
        if (MidiEvent.IsNoteOff(message.MidiEvent))
        {
            var ev = (NoteEvent)message.MidiEvent;
            NoteOff?.Invoke(ev.NoteName, ev.NoteNumber, ev.Velocity);
        }
    }

    public static int ChooseDevice()
    {
        while (true)
        {
            while (MidiIn.NumberOfDevices == 0)
            {
                Console.WriteLine("No midi devices found, press enter to try check again.");
                Console.ReadLine();
            }
            Console.WriteLine("> Midi devices:");
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                Console.WriteLine($"{device}: {MidiIn.DeviceInfo(device).ProductName}");
            }
            Console.Write("Choose one device by number: ");
            string? line = Console.ReadLine();
            string input = line is null ? "" : line;
            bool result = int.TryParse(input, out int deviceNumber);
            if (!result)
            {
                Console.WriteLine("Input must be a number, try again");
                continue;
            }
            if (deviceNumber >= MidiIn.NumberOfDevices || deviceNumber < 0)
            {
                Console.WriteLine(
                    $"Input must be in range [0 -> {MidiIn.NumberOfDevices - 1}], try again"
                );
                continue;
            }
            return deviceNumber;
        }
    }

    private readonly int _deviceNumber;
    private readonly MidiIn _midiIn;

    // Dispose pattern
    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                _midiIn.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~MidiManager()
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
