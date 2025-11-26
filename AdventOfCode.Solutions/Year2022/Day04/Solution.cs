namespace AdventOfCode.Solutions.Year2022.Day04;

class Solution : SolutionBase
{
    public Solution() : base(04, 2022, "Camp Cleanup") { }

    protected override string? SolvePartOne() => Input.SplitByNewline()
        .Select(pair =>
        {
            var (a, b, _) = pair.Split(",");

            var (sA, eA, _) = a.Split("-").Select(int.Parse).ToArray();
            var (sB, eB, _) = b.Split("-").Select(int.Parse).ToArray();

            return (sA >= sB && eA <= eB) || (sA <= sB && eA >= eB);
        })
        .Count(contained => contained).ToString();

    protected override string? SolvePartTwo() => Input.SplitByNewline()
        .Select(pair =>
        {
            var (a, b, _) = pair.Split(",");

            var (sA, eA, _) = a.Split("-").Select(int.Parse).ToArray();
            var (sB, eB, _) = b.Split("-").Select(int.Parse).ToArray();

            return (sA >= sB && eB >= sA) || (sA <= sB && eA >= sB);
        })
        .Count(overlap => overlap).ToString();
}
