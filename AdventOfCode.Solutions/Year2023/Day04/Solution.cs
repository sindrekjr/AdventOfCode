using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2023.Day04;

partial class Solution : SolutionBase
{
    public Solution() : base(04, 2023, "Scratchcards") { }

    [GeneratedRegex(@"\s+")]
    private static partial Regex SpaceRegex();

    protected override string SolvePartOne()
    {
        return Input.SplitByNewline().Select(card =>
        {
            var (_, points, _) = card.Split(":");
            var (winners, owned, _) = points
                .Split("|", StringSplitOptions.TrimEntries)
                .Select(str => SpaceRegex().Split(str).Select(int.Parse))
                .ToArray();

            var sum = 0;
            foreach (var n in owned)
            {
                if (winners.Contains(n))
                {
                    sum = sum == 0
                        ? 1
                        : sum * 2;
                }
            }

            return sum;
        }).Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
