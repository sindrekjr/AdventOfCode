namespace AdventOfCode.Solutions.Year2024.Day05;

class Solution : SolutionBase
{
    public Solution() : base(05, 2024, "Print Queue") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
