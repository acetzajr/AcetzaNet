using Acetza.Muza.Functions;
using Acetza.Muza.WaveNS;

namespace Acetza.Muza.Blocks;

public partial class Enveloper
{
    static Result Transform(
        Wave wave,
        Transformer transformer,
        double startTime,
        double startAmplitude,
        double endTime,
        double endAmplitude,
        double limitTime
    )
    {
        int frameLimit = wave.FrameRate.TimeToIndex(limitTime);
        int start = wave.FrameRate.TimeToIndex(startTime);
        int end = wave.FrameRate.TimeToIndex(endTime);
        double difference = endAmplitude - startAmplitude;
        double last = startAmplitude + transformer(0) * difference;
        double frames = end - start;
        double index = 0;
        for (int frame = start; frame < end; frame++, index++)
        {
            if (frame >= frameLimit)
                return new(true, wave.FrameRate.IndexToTime(frame), last);
            last = startAmplitude + transformer(index / frames) * difference;
            wave[frame] *= last;
        }
        return new(false, endTime, last);
    }
}
