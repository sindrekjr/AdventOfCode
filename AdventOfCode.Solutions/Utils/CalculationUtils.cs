namespace AdventOfCode.Solutions.Utils;

public static class CalculationUtils
{
    public static (int, int) Add(this (int x, int y) a, (int x, int y) b)
        => (a.x + b.x, a.y + b.y);

    public static (int, int, int) Add(this (int x, int y, int z) a, (int x, int y, int z) b)
        => (a.x + b.x, a.y + b.y, a.z + b.z);

    public static double FindGCD(double a, double b)
    {
        if (a == 0 || b == 0) return Math.Max(a, b);
        return (a % b == 0) ? b : FindGCD(b, a % b);
    }

    public static double FindLCM(double a, double b) => a * b / FindGCD(a, b);

    public static int ManhattanDistance((int x, int y) a, (int x, int y) b) =>
        Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
}
