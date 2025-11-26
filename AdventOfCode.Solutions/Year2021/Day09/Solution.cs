using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Solutions.Year2020;

namespace AdventOfCode.Solutions.Year2021.Day09;

class Solution : SolutionBase
{
    public Solution() : base(09, 2021, "Smoke Basin") { }

    protected override string? SolvePartOne()
    {
        var map = new SquareMap<int>(Input.SplitByNewline().Select(row => row.ToIntArray()).ToArray());
        return FindSinks(map)
            .Aggregate(0, (sum, kv) => sum + kv.Value + 1)
            .ToString();
    }

    protected override string? SolvePartTwo()
    {
        var map = new SquareMap<int>(Input.SplitByNewline().Select(row => row.ToIntArray()).ToArray());
        return FindSinks(map)
            .ToArray()
            .Select(kv => CalculateBasinSize(map, kv.Key))
            .OrderByDescending(size => size)
            .Take(3)
            .Aggregate(1, (f, c) => f * c)
            .ToString();
    }

    IEnumerable<KeyValuePair<(int x, int y), int>> FindSinks(SquareMap<int> map)
    {
        foreach (var kv in map)
        {
            var (position, value) = kv;
            var (x, y) = position;

            if (map.TryGetValue((x - 1, y), out var adjL) && adjL <= value) continue;
            if (map.TryGetValue((x + 1, y), out var adjR) && adjR <= value) continue;
            if (map.TryGetValue((x, y - 1), out var adjD) && adjD <= value) continue;
            if (map.TryGetValue((x, y + 1), out var adjU) && adjU <= value) continue;

            yield return kv;
        }
    }

    int CalculateBasinSize(SquareMap<int> map, (int x, int y) sink)
    {
        var (x, y) = sink;
        var size = 1;

        map.Remove(sink);
        if (map.TryGetValue((x - 1, y), out var adjL) && adjL < 9)
        {
            size += CalculateBasinSize(map, (x - 1, y));
        }

        if (map.TryGetValue((x + 1, y), out var adjR) && adjR < 9)
        {
            size += CalculateBasinSize(map, (x + 1, y));
        }

        if (map.TryGetValue((x, y - 1), out var adjD) && adjD < 9)
        {
            size += CalculateBasinSize(map, (x, y - 1));
        }

        if (map.TryGetValue((x, y + 1), out var adjU) && adjU < 9)
        {
            size += CalculateBasinSize(map, (x, y + 1));
        }

        return size;
    }
}
