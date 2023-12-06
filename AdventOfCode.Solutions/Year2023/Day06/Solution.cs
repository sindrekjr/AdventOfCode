using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2023.Day06;

partial class Solution : SolutionBase
{
    public Solution() : base(06, 2023, "Wait For It") { }

    [GeneratedRegex(@"\s+")]
    private static partial Regex SpaceRegex();

    protected override string SolvePartOne()
    {
        var (times, records, _) = Input.SplitByNewline().Select(line =>
        {
            return line.Split(":").Last().Trim().ToIntArray(SpaceRegex());
        }).ToArray();

        var result = 1;
        for (var i = 0; i < times.Length; i++)
        {
            var beat = 0;
            var time = times[i];
            var record = records[i];

            for (var hold = 1; hold < time; hold++)
            {
                var distance = (time - hold) * hold;
                if (distance > record)
                {
                    beat++;
                    continue;
                }

                if (beat > 0) break;
            }

            result *= beat;
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
