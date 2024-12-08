namespace AdventOfCode.Solutions.Year2024.Day08;

class Solution : SolutionBase
{
    public Solution() : base(08, 2024, "Resonant Collinearity") { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
