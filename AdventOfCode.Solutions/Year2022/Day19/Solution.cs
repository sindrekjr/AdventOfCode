namespace AdventOfCode.Solutions.Year2022.Day19;

class Solution : SolutionBase
{
    public Solution() : base(19, 2022, "Not Enough Minerals") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
