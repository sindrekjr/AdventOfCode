namespace AdventOfCode.Solutions.Year2024.Day14;

class Solution : SolutionBase
{
    public Solution() : base(14, 2024, "Restroom Redoubt") { }

    protected override string? SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string? SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
