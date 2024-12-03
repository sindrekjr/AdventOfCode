namespace AdventOfCode.Solutions.Year2024.Day03;

class Solution : SolutionBase
{
    public Solution() : base(03, 2024, "Mull It Over") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
