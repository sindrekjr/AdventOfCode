namespace AdventOfCode.Solutions.Year2024.Day20;

class Solution : SolutionBase
{
    public Solution() : base(20, 2024, "Race Condition") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
