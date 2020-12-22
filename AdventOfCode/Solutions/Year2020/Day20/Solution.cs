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

        public Day20() : base(20, 2020, "Jurassic Jigsaw") { }

        protected override string SolvePartOne()
        {
            Tiles = new Dictionary<int, ImageTile>();
            foreach (var (title, tile) in Input.SplitByParagraph().Select(p => p.SplitByNewline()))
            {
                var id = int.Parse(title.Substring(5, 4));
                Tiles.Add(id, ParseTile(id, tile));
            }

            foreach (var tile in Tiles.Values) FindMatches(tile);

            return Tiles.Values.Where(t => t.CountMatchingSides() == 2).Aggregate(default(long) + 1, (product, t) => product * t.Id).ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        ImageTile ParseTile(int id, IEnumerable<string> tile)
            => new ImageTile
            {
                Id = id,
                Data = tile.ToArray(),
                Top = tile.First(),
                Bottom = tile.Last(),
                Right = tile.Aggregate("", (side, line) => side + line.Last()),
                Left = tile.Aggregate("", (side, line) => side + line.First())
            };

        void FindMatches(ImageTile tile)
        {
            foreach (var t in Tiles.Values.Where(t => t.Id != tile.Id))
            {
                if (IsMatch(tile.Top, t)) tile.TopMatches.Add(t);
                if (IsMatch(tile.Right, t)) tile.RightMatches.Add(t);
                if (IsMatch(tile.Bottom, t)) tile.BottomMatches.Add(t);
                if (IsMatch(tile.Left, t)) tile.LeftMatches.Add(t);
            }
        }

        bool IsMatch(string side, ImageTile tile)
            => new string[]
            {
                tile.Top, tile.Top.Reverse(),
                tile.Right, tile.Right.Reverse(),
                tile.Bottom, tile.Bottom.Reverse(),
                tile.Left, tile.Left.Reverse(),
            }.Contains(side);

        void PrintTile(IEnumerable<string> tile) => Console.WriteLine(tile.Select(s => s + "\n").JoinAsStrings());
    }

    internal class ImageTile
    {
        public int Id { get; set; }
        public string[] Data { get; set; }
        public string Top { get; set; }
        public string Right { get; set; }
        public string Bottom { get; set; }
        public string Left { get; set; }

        public List<ImageTile> TopMatches { get; set; } = new List<ImageTile>();
        public List<ImageTile> RightMatches { get; set; } = new List<ImageTile>();
        public List<ImageTile> BottomMatches { get; set; } = new List<ImageTile>();
        public List<ImageTile> LeftMatches { get; set; } = new List<ImageTile>();

        public int CountMatchingSides()
        {
            int count = 0;
            if (TopMatches.Any()) count++;
            if (RightMatches.Any()) count++;
            if (BottomMatches.Any()) count++;
            if (LeftMatches.Any()) count++;
            return count;
        }
    }
}
