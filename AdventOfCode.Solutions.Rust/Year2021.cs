using System.Runtime.InteropServices;

namespace AdventOfCode.Solutions.Rust;

public static class Year2021
{
    [DllImport("solutions2021.dll")] private static extern void solve(int year, int day, int part);

    public static void Solve(int day, int part) => solve(2021, day, part);
}
