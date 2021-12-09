using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Solutions.Year2020;

namespace AdventOfCode.Solutions.Year2021
{
    class Day09 : ASolution
    {
        public Day09() : base(09, 2021, "Smoke Basin") { }

        protected override string SolvePartOne()
        {
            var map = new Map<int>(Input.SplitByNewline().Select(row => row.ToIntArray()).ToArray());
            return FindSinks(map).Aggregate(0, (sum, kv) => sum + kv.Value + 1).ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        IEnumerable<KeyValuePair<(int x, int y), int>> FindSinks(Map<int> map)
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
    }
}
