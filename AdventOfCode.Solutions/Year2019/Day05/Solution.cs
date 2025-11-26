namespace AdventOfCode.Solutions.Year2019.Day05;

class Solution : SolutionBase
{

    IntcodeComputer Machine;

    public Solution() : base(5, 2019, "Sunny with a Chance of Asteroids")
    {
        Machine = new IntcodeComputer(Input.ToIntArray(","));
    }

    protected override string? SolvePartOne() => Machine.Initialize().WriteInput(1).Run().Diagnose().ToString();

    protected override string? SolvePartTwo()
    {
        Machine.Initialize().WriteInput(5).Run();
        return Machine.Diagnose().ToString();
    }
}
