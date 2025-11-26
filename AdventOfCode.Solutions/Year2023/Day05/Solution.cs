namespace AdventOfCode.Solutions.Year2023.Day05;

class Solution : SolutionBase
{
    public Solution() : base(05, 2023, "If You Give A Seed A Fertilizer") { }

    protected override string? SolvePartOne()
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

    protected override string? SolvePartTwo()
    {
        var (seeds, paragraphs) = Input.SplitByParagraph();

        var seedsArray = seeds.Split(": ").Last().ToLongArray(" ");
        var seedRanges = new List<(long start, long end)>();
        for (var i = 0; i < seedsArray.Length; i++)
        {
            var start = seedsArray[i++];
            var end = start + seedsArray[i];
            seedRanges.Add((start, end));
        }

        var maps = paragraphs.Select(data =>
        {
            var map = new Dictionary<(long start, long end), (long start, long end)>();
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

        foreach (var map in maps)
        {
            var seedsExpanded = new List<(long, long)>();

            for (var i = 0; i < seedRanges.Count; i++)
            // foreach (var (start, end) in seedRanges)
            {
                var (start, end) = seedRanges[i];

                var match = map.FirstOrDefault(kv =>
                    kv.Key.start <= end && kv.Key.end >= start);

                if (match.Key.start == 0 && match.Key.end == 0)
                {
                    seedsExpanded.Add((start, end));
                    continue;
                }

                var (target, source, length) = (match.Value.start, match.Key.start, match.Key.end - match.Key.start + 1);

                if (source <= start && source + length - 1 >= end)
                {
                    seedsExpanded.Add((start - source + target, end - source + target));
                }
                else if (source > start && source + length - 1 >= end)
                {
                    seedsExpanded.Add((target, end - source + target));
                    seedRanges.Add((start, source - 1));
                }
                else if (source <= start && source + length - 1 < end)
                {
                    seedsExpanded.Add((start - source + target, target + length - 1));
                    seedRanges.Add((source + length, end));
                }
                else if (source > start && source + length - 1 < end)
                {
                    seedsExpanded.Add((target, target + length - 1));
                    seedRanges.Add((start, source - 1));
                    seedRanges.Add((source + length, end));
                }
            }

            seedRanges = seedsExpanded;
        }

        return seedRanges.Min(range => range.start).ToString();
    }

    long GetLocation(Dictionary<(long, long), (long, long)>[] maps, long seed) =>
        maps.Aggregate(seed, (seed, map) => GetLocation(map, seed));

    long GetLocation(Dictionary<(long start, long end), (long start, long end)> map, long seed)
    {
        foreach (var (source, destination) in map)
        {
            if (seed >= source.start && seed <= source.end)
            {
                return seed + destination.start - source.start;
            }
        }

        return seed;
    }
}
