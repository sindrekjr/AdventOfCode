namespace AdventOfCode.Solutions.Year2024.Day18;

class Solution : SolutionBase
{
    public Solution() : base(18, 2024, "RAM Run") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
