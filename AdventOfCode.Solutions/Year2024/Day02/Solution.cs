namespace AdventOfCode.Solutions.Year2024.Day02;

class Solution : SolutionBase
{
    public Solution() : base(02, 2024, "Red-Nosed Reports") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
