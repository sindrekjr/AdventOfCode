namespace AdventOfCode.Solutions.Year2019.Day11;

class Solution : SolutionBase
{

    HullPaintingRobot Robot;

    public Solution() : base(11, 2019, "Space Police")
    {
        Robot = new HullPaintingRobot(new IntcodeComputer(Input.ToIntArray(",")));
    }

    protected override string SolvePartOne() => Robot.Run().Count.ToString();

    protected override string SolvePartTwo()
    {
        //var Map = Robot.Initialize().Run(1); 
        return null;
    }
}
