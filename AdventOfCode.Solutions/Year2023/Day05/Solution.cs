namespace AdventOfCode.Solutions.Year2023.Day05;

class Solution : SolutionBase
{
    public Solution() : base(05, 2023, "If You Give A Seed A Fertilizer") { }

    protected override string SolvePartOne()
    {
        var (seeds, paragraphs) = Input.SplitByParagraph();

        var maps = paragraphs.Select(data =>
        {
            var map = new Dictionary<(long, long), (long, long)>();
            foreach (var line in data.SplitByNewline().Skip(1))
            {
                var values = line.ToLongArray(" ");
                var length = values[2];

                var destination =
                (
                    values[0],
                    values[0] + length - 1
                );
                var source =
                (
                    values[1],
                    values[1] + length - 1
                );
                map.Add(source, destination);
            }

            return map;
        }).ToArray();

        return seeds
            .Split(": ")
            .Last()
            .ToIntArray(" ")
            .Select(seed => GetLocation(maps, seed))
            .Min()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    long GetLocation(Dictionary<(long, long), (long, long)>[] maps, long seed)
    {
        return maps.Aggregate(seed, (source, map) =>
        {
            foreach (var ((sStart, sEnd), (dStart, dEnd)) in map)
            {
                if (source >= sStart && source <= sEnd)
                {
                    var destination = source + dStart - sStart;
                    return destination;
                }
            }

            return source;
        });
    }
}
