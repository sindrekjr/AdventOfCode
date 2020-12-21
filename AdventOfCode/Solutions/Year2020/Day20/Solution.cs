using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day20 : ASolution
    {
        Dictionary<int, ImageTile> Tiles;

        public Day20() : base(20, 2020, "Jurassic Jigsaw", true) { }

        protected override string SolvePartOne()
        {
            Tiles = new Dictionary<int, ImageTile>();
            foreach (var (id, tile) in Input.SplitByParagraph().Select(p => p.SplitByNewline()))
            {
                Tiles.Add(int.Parse(id.Substring(5, 4)), GetSides(tile));
                // Console.WriteLine(GetSides(tile).left);
                // PrintTile(tile);
            }

            foreach (var (id, tile) in Tiles)
            {
                var matchingSides = 0;
                var otherTiles = Tiles.Where(tile => tile.Key != id);

                var topMatch = FindMatch(otherTiles, tile.Top);
                if (topMatch != -1)
                {
                    matchingSides++;
                    Console.WriteLine(tile.Top + " matches: " + topMatch);
                }
                // if (topMatch == -1) continue;
                // if (topMatch != -1) otherTiles = otherTiles.Where(tile => tile.Key != topMatch);
                // else continue;

                var rightMatch = FindMatch(otherTiles, tile.Right);
                if (rightMatch != -1)
                {
                    matchingSides++;
                    Console.WriteLine(tile.Right + " matches: " + rightMatch);
                }
                // if (rightMatch == -1) continue;
                // if (rightMatch != -1) otherTiles = otherTiles.Where(tile => tile.Key != rightMatch);
                // else continue;

                var bottomMatch = FindMatch(otherTiles, tile.Bottom);
                if (bottomMatch != -1)
                {
                    matchingSides++;
                    Console.WriteLine(tile.Bottom + " matches: " + bottomMatch);
                }
                // if (bottomMatch == -1) continue;
                // if (bottomMatch != -1) otherTiles = otherTiles.Where(tile => tile.Key != bottomMatch);
                // else continue;

                var leftMatch = FindMatch(otherTiles, tile.Left);
                if (leftMatch != -1)
                {
                    matchingSides++;
                    Console.WriteLine(tile.Left + " matches: " + leftMatch);
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

        ImageTile GetSides(IEnumerable<string> tile)
            => new ImageTile
            {
                Data = tile.ToArray(),
                Top = tile.First(),
                Bottom = tile.Last().Reverse(),
                Right = tile.Aggregate("", (side, line) => side + line.Last()),
                Left = tile.Aggregate("", (side, line) => line.First() + side)
            };

        int FindMatch(IEnumerable<KeyValuePair<int, ImageTile>> tiles, string side)
        {
            var found = tiles.FirstOrDefault(kv => 
            {
                var (id, tile) = kv;
                return side == tile.Top.Reverse()
                    || side == tile.Right.Reverse()
                    || side == tile.Bottom.Reverse()
                    || side == tile.Left.Reverse();
            });

            return found.Key == default ? -1 : found.Key;
        }

        void PrintTile(IEnumerable<string> tile) => Console.WriteLine(tile.Select(s => s + "\n").JoinAsStrings());
    }

    internal class ImageTile
    {
        public string[] Data { get; set; }
        public string Top { get; set; }
        public string Right { get; set; }
        public string Bottom { get; set; }
        public string Left { get; set; }
    }
}
