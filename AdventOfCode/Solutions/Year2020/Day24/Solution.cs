using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day24 : ASolution
    {
        public Day24() : base(24, 2020, "Lobby Layout") { }

        protected override string SolvePartOne()
        {
            var reference = (0, 0, 0);
            var tiles = new Grid<bool>();
            foreach (var line in Input.SplitByNewline().Select(l => Regex.Split(l, "(nw|ne|e|w|sw|se)")))
            {
                var tile = line.Aggregate(reference, (t, inst) => t.Add(inst switch 
                {
                    "nw" => (-1, 0, 1),
                    "ne" => (0, -1, 1),
                    "w" => (-1, 1, 0),
                    "e" => (1, -1, 0),
                    "sw" => (0, 1, -1),
                    "se" => (1, 0, -1),
                    _ => (0, 0, 0),
                }));

                if (tiles.ContainsKey(tile))
                {
                    tiles[tile] = !tiles[tile];
                }
                else
                {
                    tiles.Add(tile, true);
                }
            }

            return tiles.Values.Count(t => t).ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }
    }
}
