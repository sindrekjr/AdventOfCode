using System.Linq;

namespace AdventOfCode.Solutions.Year2021.Day05;

class Solution : SolutionBase
{
    public Solution() : base(05, 2021, "Hydrothermal Venture") { }

    protected override string? SolvePartOne() =>
        ParseInput(Input.SplitByNewline()).Values.Count(value => value >= 2).ToString();

    protected override string? SolvePartTwo() =>
        ParseInput(Input.SplitByNewline(), true).Values.Count(value => value >= 2).ToString();

    Dictionary<(int x, int y), int> ParseInput(IEnumerable<string> lines, bool diagonal = false)
    {
        var map = new Dictionary<(int x, int y), int>();

        foreach (var line in lines)
        {
            var (first, second, _) = line.Split(" -> ");

            var (x1, y1, _) = first.ToIntArray(",");
            var (x2, y2, _) = second.ToIntArray(",");

            if (x1 == x2)
            {
                var (small, large) = GetSmallestAndLargest(y1, y2);

                for (var i = small; i <= large; i++)
                {
                    var pos = (x1, i);
                    if (!map.ContainsKey(pos)) map.Add(pos, 0);
                    map[pos]++;
                }
            }
            else if (y1 == y2)
            {
                var (small, large) = GetSmallestAndLargest(x1, x2);

                for (var i = small; i <= large; i++)
                {
                    var pos = (i, y1);
                    if (!map.ContainsKey(pos)) map.Add(pos, 0);
                    map[pos]++;
                }
            }
            else if (diagonal)
            {
                var (sX, lX) = GetSmallestAndLargest(x1, x2);
                var (sY, _) = GetSmallestAndLargest(y1, y2);
                var y = sX == x1 ? y1 : y2;
                var incrementY = y == sY;

                for (int x = sX; x <= lX; x++)
                {
                    var pos = (x, y);
                    if (!map.ContainsKey(pos)) map.Add(pos, 0);
                    map[pos]++;

                    if (incrementY) y++;
                    if (!incrementY) y--;
                }
            }
        }

        return map;
    }

    (int, int) GetSmallestAndLargest(int a, int b) => (a < b ? a : b, a > b ? a : b);
}
