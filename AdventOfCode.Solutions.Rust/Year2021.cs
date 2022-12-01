using System.Runtime.InteropServices;

namespace AdventOfCode.Solutions.Rust;

public static class Year2021
{
    [DllImport("solutions2021.dll", EntryPoint = "process")]
    private static extern void ProcessInRust();

    public static void Test()
    {
        ProcessInRust();
    }
}
