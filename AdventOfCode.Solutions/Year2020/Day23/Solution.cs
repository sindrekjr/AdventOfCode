namespace AdventOfCode.Solutions.Year2020.Day23;

class Solution : SolutionBase
{
    public Solution() : base(23, 2020, "Crab Cups") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}