namespace AdventOfCode.Solutions.Year2024.Day23;

class Solution : SolutionBase
{
    public Solution() : base(23, 2024, "LAN Party") { }

    protected override string? SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string? SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
