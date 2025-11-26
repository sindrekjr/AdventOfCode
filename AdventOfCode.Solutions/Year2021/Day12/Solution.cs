namespace AdventOfCode.Solutions.Year2021.Day12;

class Solution : SolutionBase
{
    Dictionary<string, HashSet<string>> Connections;

    public Solution() : base(12, 2021, "Passage Pathing")
    {
        Connections = ParseInput();
    }

    protected override string? SolvePartOne() =>
        Connections["end"].Aggregate(0, (paths, p) => paths + CountPaths(p, "end")).ToString();

    protected override string? SolvePartTwo() =>
        Connections["end"].Aggregate(0, (paths, p) => paths + CountPaths(p, "end", true)).ToString();

    int CountPaths(string cave, string path, bool canVisitSmallTwice = false)
    {
        if (cave.ToLower() == cave && canVisitSmallTwice) canVisitSmallTwice = !path.Contains(cave);
        path += $"-{cave}";

        var count = 0;
        foreach (var p in Connections[cave])
        {
            if (p == "end" || path.Contains(p) && p.ToLower() == p && !canVisitSmallTwice) continue;

            count += p == "start"
                ? 1
                : CountPaths(p, path, canVisitSmallTwice);
        }

        return count;
    }

    Dictionary<string, HashSet<string>> ParseInput() => Input.SplitByNewline().Aggregate(new Dictionary<string, HashSet<string>>(), (dict, line) =>
    {
        var (f, s, _) = line.Split("-");
        if (!dict.ContainsKey(f)) dict.Add(f, new HashSet<string>());
        if (!dict.ContainsKey(s)) dict.Add(s, new HashSet<string>());
        dict[f].Add(s);
        dict[s].Add(f);

        return dict;
    });
}
