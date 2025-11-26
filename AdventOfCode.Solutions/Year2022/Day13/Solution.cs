namespace AdventOfCode.Solutions.Year2022.Day13;

class Solution : SolutionBase
{
    public Solution() : base(13, 2022, "Distress Signal") { }

    protected override string? SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string? SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
