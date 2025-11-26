namespace AdventOfCode.Solutions.Year2022.Day15;

class Solution : SolutionBase
{
    public Solution() : base(15, 2022, "Beacon Exclusion Zone") { }

    protected override string? SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string? SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
