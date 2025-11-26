using AdventOfCode.Solutions.Year2020;

namespace AdventOfCode.Solutions.Year2023.Day03;

class Solution : SolutionBase
{
    public Solution() : base(03, 2023, "Gear Ratios") { }

    protected override string? SolvePartOne()
    {
        var schematic = ParseSchematic();
        var values = new List<int>();

        var nPositions = new List<(int, int)>();
        foreach (var (k, v) in schematic)
        {
            if (char.IsNumber(v))
            {
                nPositions.Add(k);
                continue;
            }

            if (nPositions.Count == 0)
            {
                continue;
            }

            var number = nPositions.Aggregate("", (n, pos) => n + schematic[pos]);
            var valid = nPositions.Any(pos =>
            {
                var adjacents = schematic.PokeAround(pos);
                return adjacents.Any(ch => !char.IsNumber(ch) && ch != '.');
            });

            nPositions.Clear();

            if (valid)
            {
                values.Add(int.Parse(number));
            }
        }

        return values.Sum().ToString();
    }

    protected override string? SolvePartTwo()
    {
        var schematic = ParseSchematic();
        var gears = new Dictionary<(int, int), int>();
        var parts = new SquareMap<int>();

        var nPositions = new List<(int, int)>();
        foreach (var (k, v) in schematic)
        {
            if (char.IsNumber(v))
            {
                nPositions.Add(k);
                continue;
            }

            if (v == '*')
            {
                gears.Add(k, 0);
            }

            if (nPositions.Count == 0)
            {
                continue;
            }

            var number = nPositions.Aggregate("", (n, pos) => n + schematic[pos]);
            foreach (var pos in nPositions)
            {
                parts.Add(pos, int.Parse(number));
            }

            nPositions.Clear();
        }

        foreach (var pos in gears.Keys.ToArray())
        {
            var adjacentParts = parts.PokeAround(pos).Distinct().ToArray();
            if (adjacentParts.Length == 2)
            {
                gears[pos] = adjacentParts[0] * adjacentParts[1];
                continue;
            }

            gears.Remove(pos);
        }

        return gears.Values.Sum().ToString();

    }

    SquareMap<char> ParseSchematic()
    {
        var charArray = Input
            .SplitByNewline()
            .Select(line => line.ToCharArray())
            .ToArray();
        return new SquareMap<char>(charArray);
    }
}
