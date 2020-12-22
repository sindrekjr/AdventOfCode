using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Day20 : ASolution
    {
        int Aspect;
        Map<ImageTile> Puzzle;
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
            Tiles = new Dictionary<int, ImageTile>();
            foreach (var (title, tile) in Input.SplitByParagraph().Select(p => p.SplitByNewline()))
            {
                var id = int.Parse(title.Substring(5, 4));
                Tiles.Add(id, ParseTile(id, tile.Take(tile.Count - 1).Skip(1).Select(s => s.Substring(1, s.Length - 2))));
            }
            Aspect = (int) Math.Sqrt(Tiles.Count);

            foreach (var tile in Tiles.Values) FindMatches(tile);

            CreatePuzzle();
            return Puzzle.Count.ToString();
        }

        void CreatePuzzle()
        {
            Puzzle = new Map<ImageTile>();
            var placedTile = Tiles.Values.Where(IsCornerTile).First(t => PlaceCornerTile(t, Aspect));
            var position = Puzzle.Keys.First();

            while (Tiles.Count > 0) position = PlaceNextTile(position);

            // Console.WriteLine("Left: " + placedTile.LeftMatches.Where(IsEdgeTile).Count());
            // Console.WriteLine("Bottom: " + placedTile.BottomMatches.Where(IsEdgeTile).Count());
            // Console.WriteLine((placedTile == Puzzle[(0, Aspect)]).ToString());
            // Console.WriteLine(Puzzle.Count);
        }

        (int, int) PlaceNextTile((int x, int y) position)
        {
            var previousTile = Puzzle[position];
            if (IsCornerTile(previousTile))
            {
                Console.WriteLine("Top: " + previousTile.TopMatches.Where(IsEdgeTile).Count());
                Console.WriteLine("Right: " + previousTile.RightMatches.Where(IsEdgeTile).Count());
                Console.WriteLine("Bottom: " + previousTile.BottomMatches.Where(IsEdgeTile).Count());
                Console.WriteLine("Left: " + previousTile.LeftMatches.Where(IsEdgeTile).Count());
            }

            Tiles.Remove(Tiles.Keys.First(), out ImageTile next);
            return position;
        }

        bool PlaceCornerTile(ImageTile tile, int sqrt) =>
        (
            (tile.RightMatches.Any() && tile.BottomMatches.Any() && Puzzle.TryAdd((0, 0), tile))
            || (tile.RightMatches.Any() && tile.TopMatches.Any() && Puzzle.TryAdd((sqrt, 0), tile))
            || (tile.LeftMatches.Any() && tile.BottomMatches.Any() && Puzzle.TryAdd((0, sqrt), tile))
            || (tile.LeftMatches.Any() && tile.TopMatches.Any() && Puzzle.TryAdd((sqrt, sqrt), tile))
        ) && Tiles.Remove(tile.Id);

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

        bool IsCornerTile(ImageTile tile) => tile.CountMatchingSides() == 2;
        bool IsEdgeTile(ImageTile tile) => tile.CountMatchingSides() == 3;

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
