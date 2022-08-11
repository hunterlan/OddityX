using System;

namespace OddityX.Helpers;

public record LaunchRecordInfo
{
    public string Name { get; init; }

    public bool? Upcoming { get; init; }

    public bool? Success { get; init; }

    public uint? FlightNumber { get; init; }

    public DateTime? DateUtc { get; init; }

    public DateTime? DateLocal { get; init; }

}