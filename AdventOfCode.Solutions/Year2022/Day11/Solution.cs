namespace AdventOfCode.Solutions.Year2022.Day11;

class Solution : SolutionBase
{
    public Solution() : base(11, 2022, "Monkey in the Middle") { }

    protected override string? SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string? SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
