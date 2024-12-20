namespace AdventOfCode.Solutions.Year2024.Day17;

class Solution : SolutionBase
{
    public Solution() : base(17, 2024, "Chronospatial Computer") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
