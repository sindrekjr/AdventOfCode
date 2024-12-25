namespace AdventOfCode.Solutions;

public struct SolutionResult
{
    public string Answer { get; set; }
    public TimeSpan Duration { get; set; }

    private readonly string FormattedTime =>
        Duration.Microseconds < 1 ? $"{Duration.Nanoseconds}ns" :
        Duration.TotalMilliseconds < 1 ? $"{Duration.TotalMicroseconds}µs" :
        Duration.TotalSeconds < 1 ? $"{Duration.TotalMilliseconds}ms" :
        $"{Duration.TotalSeconds}s";

    public override readonly string ToString() => string.IsNullOrEmpty(Answer)
        ? "Unsolved"
        : $"{Answer} ({FormattedTime})";

    public static SolutionResult Empty => new();
}
