﻿namespace Acetza.Muza.Wave;

public readonly struct FrameRate(int value) : IValueGetter<int>
{
    public FrameRate() : this(44_100) { }
    public int Value { get; } = value;
    public int TimeToIndex(double time) { return (int)(time * Value); }
    public double IndexToTime(int index) { return (double)index / Value; }
}