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
}
