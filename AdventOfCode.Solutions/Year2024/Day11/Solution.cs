namespace AdventOfCode.Solutions.Year2024.Day11;

class Solution : SolutionBase
{
    public Solution() : base(11, 2024, "Plutonian Pebbles") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
