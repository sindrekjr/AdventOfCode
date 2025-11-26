namespace AdventOfCode.Solutions.Year2020.Day12;

class Solution : SolutionBase
{
    (int x, int y) StartingPosition = (0, 0);

    public Solution() : base(12, 2020, "Rain Risk") { }

    protected override string? SolvePartOne()
        => ManhattanDistance(
            StartingPosition,
            Input.SplitByNewline().Aggregate(new Ship(StartingPosition), (ship, action) => ship.DoActionByErroneousAssumptions(action)).Position
        ).ToString();

    protected override string? SolvePartTwo()
        => ManhattanDistance(
            StartingPosition,
            Input.SplitByNewline().Aggregate(new Ship(StartingPosition, StartingPosition.Add((-1, 10))), (ship, action) => ship.DoAction(action)).Position
        ).ToString();
}
