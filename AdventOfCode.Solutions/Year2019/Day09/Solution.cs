namespace AdventOfCode.Solutions.Year2019.Day09;

class Solution : SolutionBase
{

    IntcodeComputer Machine;

    public Solution() : base(9, 2019, "Sensor Boost")
    {
        Machine = new IntcodeComputer(Input.ToIntArray(","));
    }

    protected override string? SolvePartOne() => Machine.Initialize(1030).WriteInput(1).Run().Output.Dequeue().ToString();
    protected override string? SolvePartTwo() => Machine.Initialize(1077).WriteInput(2).Run().Output.Dequeue().ToString();
}
