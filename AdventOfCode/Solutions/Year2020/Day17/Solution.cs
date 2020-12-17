using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Day17 : ASolution
    {
        public Day17() : base(17, 2020, "Conway Cubes", true) { }

        protected override string SolvePartOne()
        {
            var grid = GetPocketDimensionInitialState();

            for (int i = 0; i < 6; i++)
            {
                grid = SimulateCycle(grid);
                // Console.WriteLine(grid.Count(c => c.Value));
            }

            // foreach (var (key, val) in grid) Console.WriteLine($"{key}: {val}");

            return grid.Count(c => c.Value).ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        Grid<bool> GetPocketDimensionInitialState()
            => new Grid<bool>(
                Input
                    .SplitByNewline()
                    .Select(x => x.Select(y => new bool[] { y == '#' }).ToArray())
                    .ToArray());

        Grid<bool> SimulateCycle(Grid<bool> original)
        {
            var grid = new Grid<bool>();

            foreach (var (key, cube) in original)
            {
                var adjacent = original.PeekAround(key).Aggregate(0, (acc, adj) => adj.Aggregate(acc, (acc, a) => a ? acc + 1 : acc));
                grid.Add(key, adjacent == 3 || (cube && adjacent == 2));
            }

            foreach (var (key, cube) in original.InfiniteChildren)
            {
                var adjacent = original.PeekAround(key).Aggregate(0, (acc, adj) => adj.Aggregate(acc, (acc, a) => a ? acc + 1 : acc));
                grid.Add(key, adjacent == 3 || (cube && adjacent == 2));
            }

            return grid;
        }

        void PrintLayerZ(Grid<bool> grid, int z = 0)
        {
            for (int x = 0;; x++)
            {
                var y = 0;
                bool hasValue = false;
                while (grid.TryGetValue((x, y++, z), out bool v))
                {
                    hasValue = true;
                    Console.Write(v ? '#' : '.');
                }
                Console.WriteLine();

                if (!hasValue) break;
            }
        }
    }
}
