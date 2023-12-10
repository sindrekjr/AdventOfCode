using AdventOfCode.Solutions.Year2020;

namespace AdventOfCode.Solutions.Year2023.Day10;

class Solution : SolutionBase
{
    public Solution() : base(10, 2023, "Pipe Maze") { }

    protected override string SolvePartOne()
    {
        var map = new SquareMap<char>(Input.SplitByNewline().Select(line => line.ToCharArray()).ToArray());
        var start = map.First(kv => kv.Value == 'S');
        return (FindLoop(map, [start.Key], start.Key).Length / 2).ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    (int, int)[] FindLoop(SquareMap<char> map, IEnumerable<(int, int)> loop, (int, int) last)
    {
        var (x, y) = last;
        (int, int)[] candidates = map[last] switch
        {
            '|' => [(x + 1, y), (x - 1, y)],
            '-' => [(x, y + 1), (x, y - 1)],
            'L' => [(x - 1, y), (x, y + 1)],
            'J' => [(x - 1, y), (x, y - 1)],
            '7' => [(x + 1, y), (x, y - 1)],
            'F' => [(x + 1, y), (x, y + 1)],
            'S' => [(x + 1, y), (x - 1, y), (x, y + 1), (x, y - 1)],

            _ => throw new InvalidOperationException(),
        };

        if (loop.Count() > 2 && candidates.Any(c => map.GetValueOrDefault(c) == 'S'))
        {
            return loop.ToArray();
        }

        var next = candidates.Where(c => !loop.Contains(c)).First();
        return FindLoop(map, loop.Append(next), next);
    }
}
