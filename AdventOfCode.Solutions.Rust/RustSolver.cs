using System.Runtime.InteropServices;

namespace AdventOfCode.Solutions.Rust;

public static class RustSolver
{
    [DllImport("solutions.dll")] private static extern nint solve(int year, int day, int part, nint input);

    public static string Solve(int year, int day, int part, string input)
    {
        var input_pntr = Marshal.StringToHGlobalAuto(input);
        var solution_pntr = solve(year, day, part, input_pntr);
        return Marshal.PtrToStringAnsi(solution_pntr) ?? "";
    }
}
