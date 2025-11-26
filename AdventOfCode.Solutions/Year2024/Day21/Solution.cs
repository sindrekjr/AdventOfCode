namespace AdventOfCode.Solutions.Year2024.Day21;

class Solution : SolutionBase
{
    public Solution() : base(21, 2024, "Keypad Conundrum") { }

    protected override string? SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string? SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
