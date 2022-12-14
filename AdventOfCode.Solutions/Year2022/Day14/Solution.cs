namespace AdventOfCode.Solutions.Year2022.Day14;

class Solution : SolutionBase
{
    public Solution() : base(14, 2022, "Regolith Reservoir") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
