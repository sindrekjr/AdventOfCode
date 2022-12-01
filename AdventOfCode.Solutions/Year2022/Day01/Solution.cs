namespace AdventOfCode.Solutions.Year2022.Day01;

class Solution : SolutionBase
{
    public Solution() : base(01, 2022, "Calorie Counting") { }

    protected override string SolvePartOne() => Input
        .SplitByParagraph()
        .Select(elf => elf.SplitByNewline().Select(int.Parse).Sum())
        .Max()
        .ToString();

    protected override string SolvePartTwo() => Input
        .SplitByParagraph()
        .Select(p => p.SplitByNewline().Select(int.Parse).Sum())
        .OrderByDescending(elf => elf)
        .Take(3)
        .Sum()
        .ToString();
}
