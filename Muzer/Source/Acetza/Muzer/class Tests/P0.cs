using Muza.Preprocess.Blocks;
using Muza.Preprocess.Notes;
using Muza.Preprocess.Synths;
using Muza.Preprocess.WaveNS;
using Rationals;

namespace Acetza.Muzer;

public delegate void TestFn();

static partial class Tests
{
    public static TestFn Selected = TestSynth;

    public static void TestScale()
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

    public static void TestSynth()
    {
        var scale = Scale.Acetza();
        var synth = new Synth1();
        var step = (Rational)1 / 4;
        double time = 0;
        var wave = new Wave();
        synth.Duration = (double)step;
        for (int i = 0; i < scale.Count; i++)
        {
            synth.Frequency = scale.Frequency(i);
            Console.WriteLine(synth.Frequency);
            wave.Add(synth.Wave, time);
            time += (double)step;
        }
        wave.Normalize().Play();
    }

    public static void Basic()
    {
        new Basic().Wave.Save();
    }

    public static void Enveloper()
    {
        new Enveloper().Wave.Save();
    }

    public static void Harmonizer()
    {
        new Harmonizer().Wave.Save();
    }

    public static void Blocks()
    {
        new Basic().Wave.Save("basic");
        new Enveloper().Wave.Save("enveloper");
        new Harmonizer().Wave.Save("harmonizer");
        new Synth1().Wave.Save("synth1");
        new Wave().Add(new Basic().Wave).Save("empty");
    }
}
