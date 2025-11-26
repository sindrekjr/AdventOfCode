namespace AdventOfCode.Solutions.Year2024.Day25;

class Solution : SolutionBase
{
    public Solution() : base(25, 2024, "Code Chronicle") { }

    protected override string? SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string? SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
