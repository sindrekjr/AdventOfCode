namespace AdventOfCode.Solutions.Year2022.Day18;

class Solution : SolutionBase
{
    public Solution() : base(18, 2022, "Boiling Boulders", true) { }

    protected override string SolvePartOne() => RustSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => RustSolver.Solve(Year, Day, 2, Input);
}
