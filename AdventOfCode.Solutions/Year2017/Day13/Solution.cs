namespace AdventOfCode.Solutions.Year2017.Day13;

class Solution : SolutionBase
{
    public Solution() : base(13, 2017, "") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
