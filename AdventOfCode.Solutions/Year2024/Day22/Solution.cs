namespace AdventOfCode.Solutions.Year2024.Day22;

class Solution : SolutionBase
{
    public Solution() : base(22, 2024, "") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
