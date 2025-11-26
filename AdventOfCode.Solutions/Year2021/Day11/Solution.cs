
using AdventOfCode.Solutions.Year2020;

namespace AdventOfCode.Solutions.Year2021.Day11;

class Solution : SolutionBase
{
    public Solution() : base(11, 2021, "Dumbo Octopus") { }

    protected override string? SolvePartOne()
    {
        var octopuses = new SquareMap<int>(Input.SplitByNewline().Select(line => line.ToIntArray()).ToArray());
        return Enumerable.Range(0, 100).Aggregate(0, (flashes, _) => flashes + FlashStep(octopuses)).ToString();
    }

    protected override string? SolvePartTwo()
    {
        var octopuses = new SquareMap<int>(Input.SplitByNewline().Select(line => line.ToIntArray()).ToArray());
        for (int i = 1;; i++) if (FlashStep(octopuses) == octopuses.Count) return i.ToString();
    }

    int FlashStep(SquareMap<int> octopuses)
    {
        var primed = new HashSet<(int, int)>();
        var positions = octopuses.Keys.ToArray();

        foreach (var pos in positions) octopuses[pos]++;

        while (octopuses.Any(o => o.Value > 9 && !primed.Contains(o.Key)))
        {
            var (pos, _) = octopuses.First(o => o.Value > 9 && !primed.Contains(o.Key));
            foreach (var adj in FindAdjacentOctopuses(octopuses, pos)) octopuses[adj]++;
            primed.Add(pos);
        }

        return positions.Count(pos =>
        {
            if (octopuses[pos] > 9)
            {
                octopuses[pos] = 0;
                return true;
            }

            return false;
        });
    }

    IEnumerable<(int x, int y)> FindAdjacentOctopuses(SquareMap<int> octopuses, (int x, int y) position)
    {
        for (int x = -1; x <= 1; x++) for (int y = -1; y <= 1; y++)
        {
            if (x == 0 && y == 0) continue;
            var (aX, aY) = position.Add((x, y));
            if (octopuses.ContainsKey((aX, aY))) yield return (aX, aY);
        }
    }
}
