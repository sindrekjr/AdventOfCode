using System.Runtime.InteropServices;

namespace AdventOfCode.Solutions.Rust;

public static class Year2021
{
    [DllImport("solutions2021.dll", EntryPoint = "solve")]
    private static extern void Solve();

    public static void Test()
    {
        Solve();
    }
}
