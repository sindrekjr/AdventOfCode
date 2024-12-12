namespace AdventOfCode.Solutions.Year2024.Day12;

class Solution : SolutionBase
{
    public Solution() : base(12, 2024, "Garden Groups") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
