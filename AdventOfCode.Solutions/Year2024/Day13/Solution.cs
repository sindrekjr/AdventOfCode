namespace AdventOfCode.Solutions.Year2024.Day13;

class Solution : SolutionBase
{
    public Solution() : base(13, 2024, "Claw Contraption") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
