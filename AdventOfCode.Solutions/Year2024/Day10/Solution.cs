namespace AdventOfCode.Solutions.Year2024.Day10;

class Solution : SolutionBase
{
    public Solution() : base(10, 2024, "Hoof It") { }

    protected override string? SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string? SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
