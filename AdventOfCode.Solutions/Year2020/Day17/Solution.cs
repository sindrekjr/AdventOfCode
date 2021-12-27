using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020.Day17;

class Solution : SolutionBase
{
    public Solution() : base(17, 2020, "Conway Cubes") { }

    protected override string SolvePartOne()
    {
        var grid = GetPocketDimensionInitialState();
        for (int i = 0; i < 6; i++) grid = SimulateCycle(grid);
        return grid.Count(c => c.Value).ToString();
    }

    protected override string SolvePartTwo()
    {
        var dimension = GetActualPocketDimensionInitialState();
        for (int i = 0; i < 6; i++) dimension = SimulateCycle(dimension);
        return dimension.Count(c => c.Value).ToString();
    }

    Grid<bool> GetPocketDimensionInitialState()
        => new Grid<bool>(
            Input
                .SplitByNewline()
                .Select(x => x.Select(y => new bool[] { y == '#' }).ToArray())
                .ToArray());

    PocketDimension<bool> GetActualPocketDimensionInitialState()
        => new PocketDimension<bool>(
            Input
                .SplitByNewline()
                .Select(x => x.Select(y => new bool[][] { new bool[] { y == '#'} }).ToArray())
                .ToArray());

    Grid<bool> SimulateCycle(Grid<bool> original)
    {
        var grid = new Grid<bool>();

        foreach (var (key, cube) in original)
        {
            var adjacent = original.PokeAround(key).Aggregate(0, (acc, adj) => adj.Aggregate(acc, (acc, a) => a ? acc + 1 : acc));
            grid.Add(key, adjacent == 3 || (cube && adjacent == 2));
        }

        foreach (var (key, cube) in new Grid<bool>(original.InfiniteChildren))
        {
            var adjacent = original.PokeAround(key).Aggregate(0, (acc, adj) => adj.Aggregate(acc, (acc, a) => a ? acc + 1 : acc));
            grid.Add(key, adjacent == 3 || (cube && adjacent == 2));
        }

        return grid;
    }

    PocketDimension<bool> SimulateCycle(PocketDimension<bool> original)
    {
        var dimension = new PocketDimension<bool>();

        foreach (var (key, cube) in original)
        {
            var adjacent = original.PokeAround(key).Aggregate(0, (acc, adj) => adj.Aggregate(acc, (acc, a) => a ? acc + 1 : acc));
            dimension.Add(key, adjacent == 3 || (cube && adjacent == 2));
        }

        foreach (var (key, cube) in new PocketDimension<bool>(original.InfiniteChildren))
        {
            var adjacent = original.PokeAround(key).Aggregate(0, (acc, adj) => adj.Aggregate(acc, (acc, a) => a ? acc + 1 : acc));
            dimension.Add(key, adjacent == 3 || (cube && adjacent == 2));
        }

        return dimension;
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
