namespace AdventOfCode.Solutions.Year2024.Day15;

class Solution : SolutionBase
{
    public Solution() : base(15, 2024, "Warehouse Woes") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
