namespace AdventOfCode.Solutions.Year2021
{
    class Day12 : ASolution
    {
        Dictionary<string, HashSet<string>> Connections;

        public Day12() : base(12, 2021, "Passage Pathing")
        {
            Connections = ParseInput();
        }

        protected override string SolvePartOne() =>
            Connections["end"].Aggregate(0, (paths, p) => paths + CountPaths(p, "end")).ToString();

        protected override string SolvePartTwo()
        {
            return null;
        }

        int CountPaths(string cave, string path)
        {
            path += $"-{cave}";

            var count = 0;
            foreach (var p in Connections[cave])
            {
                if (path.Contains(p) && p.ToLower() == p) continue;
                count += p == "start"
                    ? 1
                    : CountPaths(p, path);
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
}
