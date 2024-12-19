namespace AdventOfCode.Solutions.Year2024.Day19;

class Solution : SolutionBase
{
    public Solution() : base(19, 2024, "Linen Layout") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
