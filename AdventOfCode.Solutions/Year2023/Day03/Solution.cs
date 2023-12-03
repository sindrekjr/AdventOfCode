using AdventOfCode.Solutions.Year2020;

namespace AdventOfCode.Solutions.Year2023.Day03;

class Solution : SolutionBase
{
    public Solution() : base(03, 2023, "Gear Ratios") { }

    protected override string SolvePartOne()
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

    protected override string SolvePartTwo()
    {
        return "";
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
