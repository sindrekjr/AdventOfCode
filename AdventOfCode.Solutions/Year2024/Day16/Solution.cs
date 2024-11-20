namespace AdventOfCode.Solutions.Year2024.Day16;

class Solution : SolutionBase
{
    public Solution() : base(16, 2024, "") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
