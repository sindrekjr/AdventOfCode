using System.Runtime.InteropServices;

namespace AdventOfCode.Solutions.Rust;

public static partial class RustSolver
{
    [LibraryImport("rustsolutions.dll")]
    private static partial nint solve(int year, int day, int part, nint input);
    // private static partial nint solve(int year, int day, int part, nint input, nint debug);

    public static string? Solve(int year, int day, int part, string input)
    {
        var input_pntr = Marshal.StringToHGlobalAuto(input);
        var solution_pntr = solve(year, day, part, input_pntr);
        return Marshal.PtrToStringAnsi(solution_pntr);
    }

    [LibraryImport("rustsolutions.dll")]
    private static partial nint get_last_duration(int year, int day, int part);

    public static TimeSpan? GetSolveDuration(int year, int day, int part)
    {
        var time_pntr = get_last_duration(year, day, part);
        var time_str = Marshal.PtrToStringAnsi(time_pntr);
        return ParseDuration(time_str);
    }

    private static TimeSpan? ParseDuration(string? time_str)
    {
        if (string.IsNullOrEmpty(time_str)) return null;

        if (time_str.EndsWith("ns"))
        {
            if (long.TryParse(time_str[..^2], out var ns))
            {
                return TimeSpan.FromTicks(ns / 100);
            }
        }
        else if (time_str.EndsWith("µs"))
        {
            if (double.TryParse(time_str[..^2], out var µs))
            {
                return TimeSpan.FromTicks((long)(µs * 10));
            }
        }
        else if (time_str.EndsWith("ms"))
        {
            if (double.TryParse(time_str[..^2], out var ms))
            {
                return TimeSpan.FromMilliseconds(ms);
            }
        }
        else if (time_str.EndsWith("s"))
        {
            if (double.TryParse(time_str[..^1], out var s))
            {
                return TimeSpan.FromSeconds(s);
            }
        }

        return null;
    }
}
