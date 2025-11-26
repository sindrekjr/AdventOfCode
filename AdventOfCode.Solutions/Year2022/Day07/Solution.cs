namespace AdventOfCode.Solutions.Year2022.Day07;

class Solution : SolutionBase
{
    public Solution() : base(07, 2022, "No Space Left On Device") { }

    protected override string? SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string? SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
