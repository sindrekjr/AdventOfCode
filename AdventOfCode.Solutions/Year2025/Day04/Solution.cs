namespace AdventOfCode.Solutions.Year2025.Day04;

class Solution() : SolutionBase(04, 2025, "")
{
    protected override string SolvePartOne() => ZigSolver.Solve(Year, Day, 1, Input);

    protected override string SolvePartTwo() => ZigSolver.Solve(Year, Day, 2, Input);
}
