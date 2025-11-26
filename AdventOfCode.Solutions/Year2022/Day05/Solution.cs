namespace AdventOfCode.Solutions.Year2022.Day05;

class Solution : SolutionBase
{
    public Solution() : base(05, 2022, "Supply Stacks") { }

    protected override string? SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string? SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
