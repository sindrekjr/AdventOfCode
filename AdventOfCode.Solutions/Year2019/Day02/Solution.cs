namespace AdventOfCode.Solutions.Year2019.Day02;

class Solution : SolutionBase
{

    IntcodeComputer Machine;

    public Solution() : base(2, 2019, "1202 Program Alarm")
    {
        Machine = new IntcodeComputer(Input.ToIntArray(","));
    }

    protected override string SolvePartOne() => Machine.Initialize().SetMemory(1, 12).SetMemory(2, 02).Run().Memory[0].ToString();

    protected override string SolvePartTwo()
    {
        int noun = 0;
        while(true)
        {
            int verb = 0;
            while(verb <= 100)
            {
                if(Machine.Initialize().SetMemory(1, noun).SetMemory(2, verb).Run().Memory[0] == 19690720)
                {
                    return (100 * noun + verb).ToString();
                }
                verb++;
            }
            noun++;
        }
    }
}
