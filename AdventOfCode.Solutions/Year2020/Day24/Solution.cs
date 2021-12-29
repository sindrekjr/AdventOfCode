using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Year2020;

namespace AdventOfCode.Solutions.Year2020.Day24;

class Solution : SolutionBase
{
    public Solution() : base(24, 2020, "Lobby Layout") { }

    protected override string SolvePartOne()
        => GetFloorGrid().Values.Count(t => t).ToString();

    protected override string SolvePartTwo()
    {
        var floor = GetFloorGrid();
        for (int i = 0; i < 100; i++) floor = SimulateDay(floor);
        return floor.Values.Count(t => t).ToString();
    }

    Grid<bool> SimulateDay(Grid<bool> original)
    {
        var grid = new Grid<bool>();

        foreach (var (key, tile) in original)
        {
            var adjacent = original.PokeAround(key, 1, false).Count(t => t.First());
            grid.Add(key, adjacent == 2 || (tile && adjacent == 1));
        }

        if (original.InfiniteChildren != null)
        {
            foreach (var (key, tile) in new Grid<bool>(original.InfiniteChildren))
            {
                var adjacent = original.PokeAround(key, 1, false).Count(t => t.First());
                grid.Add(key, adjacent == 2 || (tile && adjacent == 1));
            }
        }

        return grid;
    }

    Grid<bool> GetFloorGrid()
        => ParseInstructions().Aggregate(new Grid<bool>(), (grid, inst) =>
        {
            var tile = FindTileCoordinates(inst);
            if (!grid.ContainsKey(tile)) grid.Add(tile, false);
            grid[tile] = !grid[tile];
            return grid;
        });

    (int, int, int) FindTileCoordinates(string[] directions)
        => directions.Aggregate((0, 0, 0), (t, d) => t.Add(d switch 
        {
            "nw" => (-1, 0, 1),
            "ne" => (0, -1, 1),
            "w" => (-1, 1, 0),
            "e" => (1, -1, 0),
            "sw" => (0, 1, -1),
            "se" => (1, 0, -1),
            _ => (0, 0, 0),
        }));

    IEnumerable<string[]> ParseInstructions()
        => Input.SplitByNewline().Select(l => Regex.Split(l, "(nw|ne|e|w|sw|se)"));
}
