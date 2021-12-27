namespace AdventOfCode.Solutions.Year2019.Day14;

class Solution : SolutionBase
{

    Nanofactory Factory;

    public Solution() : base(14, 2019, "Space Stoichiometry")
    {
        Factory = new Nanofactory(Input.SplitByNewline());
    }

    protected override string SolvePartOne() => Factory.Initialize().CountRequiredOre(Factory.Reactions["FUEL"], 1).ToString();

    protected override string SolvePartTwo()
    {
        Factory.Initialize().CollectOre(1000000000000);
        return Factory.ProduceMaxFuel().ToString();
    }
}
