namespace AdventOfCode.Solutions.Year2024.Day04;

class Solution : SolutionBase
{
    public Solution() : base(04, 2024, "Ceres Search") { }

    protected override string? SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string? SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
