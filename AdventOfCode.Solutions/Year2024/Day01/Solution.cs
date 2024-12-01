namespace AdventOfCode.Solutions.Year2024.Day01;

class Solution : SolutionBase
{
    public Solution() : base(01, 2024, "Historian Hysteria") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
