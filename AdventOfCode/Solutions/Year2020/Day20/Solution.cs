using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day20 : ASolution
    {
        Dictionary<int, (string Top, string Right, string Bottom, string Left)> TileSides;

        public Day20() : base(20, 2020, "Jurassic Jigsaw", true) { }

        protected override string SolvePartOne()
        {
            TileSides = new Dictionary<int, (string, string, string, string)>();
            foreach (var (id, tile) in Input.SplitByParagraph().Select(p => p.SplitByNewline()))
            {
                TileSides.Add(int.Parse(id.Substring(5, 4)), GetSides(tile));
                // Console.WriteLine(GetSides(tile).left);
                // PrintTile(tile);
            }

            foreach (var (id, sides) in TileSides)
            {
                var matchingSides = 0;
                var (top, right, bottom, left) = sides;
                var otherTiles = TileSides.Where(tile => tile.Key != id);

                var topMatch = FindMatch(otherTiles, top);
                if (topMatch != -1)
                {
                    matchingSides++;
                    Console.WriteLine(top + " matches: " + topMatch);
                }
                // if (topMatch == -1) continue;
                // if (topMatch != -1) otherTiles = otherTiles.Where(tile => tile.Key != topMatch);
                // else continue;

                var rightMatch = FindMatch(otherTiles, right);
                if (rightMatch != -1)
                {
                    matchingSides++;
                    Console.WriteLine(right + " matches: " + rightMatch);
                }
                // if (rightMatch == -1) continue;
                // if (rightMatch != -1) otherTiles = otherTiles.Where(tile => tile.Key != rightMatch);
                // else continue;

                var bottomMatch = FindMatch(otherTiles, bottom);
                if (bottomMatch != -1)
                {
                    matchingSides++;
                    Console.WriteLine(bottom + " matches: " + bottomMatch);
                }
                // if (bottomMatch == -1) continue;
                // if (bottomMatch != -1) otherTiles = otherTiles.Where(tile => tile.Key != bottomMatch);
                // else continue;

                var leftMatch = FindMatch(otherTiles, left);
                if (leftMatch != -1)
                {
                    matchingSides++;
                    Console.WriteLine(left + " matches: " + leftMatch);
                }
                // if (leftMatch == -1) continue;
                // if (leftMatch != -1) otherTiles = otherTiles.Where(tile => tile.Key != leftMatch);
                // else continue;

                if (matchingSides == 2) Console.WriteLine(id);
            }

            return null;
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        (string Top, string Right, string Bottom, string Left) GetSides(IEnumerable<string> tile)
            =>
            (
                tile.First(),
                tile.Aggregate("", (side, line) => side + line.Last()),
                tile.Last().Reverse(),
                tile.Aggregate("", (side, line) => line.First() + side)                
            );

        int FindMatch(IEnumerable<KeyValuePair<int, (string Top, string Right, string Bottom, string Left)>> tiles, string side)
        {
            var found = tiles.FirstOrDefault(kv => 
            {
                var (id, tile) = kv;
                var (t, r, b, l) = tile;
                return side == t.Reverse() || side == r.Reverse() || side == b.Reverse() || side == l.Reverse();
            });

            return found.Key == default ? -1 : found.Key;
        }

        void PrintTile(IEnumerable<string> tile) => Console.WriteLine(tile.Select(s => s + "\n").JoinAsStrings());
    }
}
