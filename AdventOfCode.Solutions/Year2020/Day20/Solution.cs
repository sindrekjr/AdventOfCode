using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions.Year2020;

namespace AdventOfCode.Solutions.Year2020.Day20;

class Solution : SolutionBase
{
    SquarePuzzle Puzzle = new(0);
    Dictionary<int, SquareTile> Tiles = new();

    public Solution() : base(20, 2020, "Jurassic Jigsaw", true) { }

    protected override string SolvePartOne()
    {
        Tiles = new Dictionary<int, SquareTile>();
        foreach (var (title, tile) in Input.SplitByParagraph().Select(p => p.SplitByNewline()))
        {
            var id = int.Parse(title.Substring(5, 4));
            Tiles.Add(id, new SquareTile(tile) { Id = id });
        }

        foreach (var tile in Tiles.Values) FindMatches(tile);

        return Tiles.Values.Where(t => t.CountMatchingSides() == 2).Aggregate(default(long) + 1, (product, t) => product * t.Id).ToString();
    }

    protected override string SolvePartTwo()
    {
        Tiles = new Dictionary<int, SquareTile>();
        foreach (var (title, tile) in Input.SplitByParagraph().Select(p => p.SplitByNewline()))
        {
            var id = int.Parse(title.Substring(5, 4));
            Tiles.Add(id, new SquareTile(tile.Take(tile.Count - 1).Skip(1).Select(s => s.Substring(1, s.Length - 2))) { Id = id });
        }

        foreach (var tile in Tiles.Values) FindMatches(tile);

        CreatePuzzle();

        return Puzzle.Count.ToString();
    }

    void CreatePuzzle()
    {
        Puzzle = new SquarePuzzle((int) Math.Sqrt(Tiles.Count) - 1);
        foreach (var t in Tiles.Values.Where(IsCornerTile)) PlaceCornerTile(t);
        // var placedTile = Tiles.Values.Where(IsCornerTile).First(t => PlaceCornerTile(t));

        while (Tiles.Any())
        {
            PlaceNextTile(Puzzle.Frontier.Dequeue());
            Console.WriteLine(Puzzle.ToString());
        }
    }

    bool PlaceNextTile((int x, int y) position)
    {
        var idealTile = Puzzle.GetNeighbouringSides(position);
        var candidates = Tiles.Values.Where(t => (!IsCornerPosition(position) || IsCornerTile(t)) && (!IsEdgePosition(position) || IsEdgeTile(t)) && t.PossibleFullMatch(idealTile));

        foreach (var c in candidates)
        {
            if (Puzzle.Fit(position, c)) return Tiles.Remove(c.Id);
        }

        return false;
    }

    bool PlaceCornerTile(SquareTile tile) =>
    (
        (tile.RightMatches.Any() && tile.BottomMatches.Any() && Puzzle.Fit((0, 0), tile))
        || (tile.RightMatches.Any() && tile.TopMatches.Any() && Puzzle.Fit((Puzzle.Bounds, 0), tile))
        || (tile.LeftMatches.Any() && tile.BottomMatches.Any() && Puzzle.Fit((0, Puzzle.Bounds), tile))
        || (tile.LeftMatches.Any() && tile.TopMatches.Any() && Puzzle.Fit((Puzzle.Bounds, Puzzle.Bounds), tile))
    ) && Tiles.Remove(tile.Id);

    void FindMatches(SquareTile tile)
    {
        foreach (var t in Tiles.Values.Where(t => t.Id != tile.Id))
        {
            if (t.AnyMatch(tile.Top)) tile.TopMatches.Add(t);
            if (t.AnyMatch(tile.Right)) tile.RightMatches.Add(t);
            if (t.AnyMatch(tile.Bottom)) tile.BottomMatches.Add(t);
            if (t.AnyMatch(tile.Left)) tile.LeftMatches.Add(t);
        }
    }

    bool IsCornerTile(SquareTile tile) => tile.CountMatchingSides() == 2;
    bool IsEdgeTile(SquareTile tile) => tile.CountMatchingSides() == 3;

    bool IsCornerPosition((int x, int y) p) => (p.x == 0 || p.x == Puzzle.Bounds) && (p.y == 0 || p.y == Puzzle.Bounds);
    bool IsEdgePosition ((int x, int y) p) => !IsCornerPosition(p) && (p.x == 0 || p.x == Puzzle.Bounds || p.y == 0 || p.y == Puzzle.Bounds);
}
