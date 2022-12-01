namespace AdventOfCode.Solutions.Year2022.Day01;

class Solution : SolutionBase
{
    public Solution() : base(01, 2022, "") { }

    protected override string SolvePartOne()
    {
        var input = Input.SplitByParagraph().Select(p => p.SplitByNewline().Select(int.Parse).Sum());
        return input.Max().ToString();
    }

    protected override string SolvePartTwo()
    {
        var input = Input.SplitByParagraph().Select(p => p.SplitByNewline().Select(int.Parse).Sum());
        return input.OrderByDescending(elf => elf).Take(3).Sum().ToString();
    }
}
