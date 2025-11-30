using System.Runtime.InteropServices;

namespace AdventOfCode.Solutions.Zig;

public static partial class ZigSolver
{
    [LibraryImport("zigsolutions.dll")]
    private static partial nint solve(int year, int day, int part, nint input);

    public static string? Solve(int year, int day, int part, string input)
    {
        var input_pntr = Marshal.StringToHGlobalAuto(input);
        var solution_pntr = solve(year, day, part, input_pntr);
        return Marshal.PtrToStringAnsi(solution_pntr);
    }

    [LibraryImport("zigsolutions.dll")]
    private static partial nint getDuration(int year, int day, int part);

    public static TimeSpan? GetSolveDuration(int year, int day, int part)
    {
        var time_pntr = getDuration(year, day, part);
        var time_str = Marshal.PtrToStringAnsi(time_pntr);
        return long.TryParse(time_str, out var ns)
            ? TimeSpan.FromTicks(ns / 100)
            : null;
    }
}
