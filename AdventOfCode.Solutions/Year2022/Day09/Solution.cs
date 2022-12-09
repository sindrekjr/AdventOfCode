namespace AdventOfCode.Solutions.Year2022.Day09;

class Solution : SolutionBase
{
    public Solution() : base(09, 2022, "Rope Bridge") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
