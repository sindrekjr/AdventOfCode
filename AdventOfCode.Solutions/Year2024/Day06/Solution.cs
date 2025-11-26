namespace AdventOfCode.Solutions.Year2024.Day06;

class Solution : SolutionBase
{
    public Solution() : base(06, 2024, "Guard Gallivant") { }

    protected override string? SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string? SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
