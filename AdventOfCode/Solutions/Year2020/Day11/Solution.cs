using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day11 : ASolution
    {

        public Day11() : base(11, 2020, "Seating System", true) { }

        protected override string SolvePartOne()
        {
            var seating = GetSeatingArrangementDict();
            bool stable = false;
            int occupied = 0;
            while (!stable)
            {
                (seating, occupied, stable) = ProcessSeatingRoundV2(seating, 4);

                Console.WriteLine($"Occupied: {occupied}");
                PrintSeatingArrangementV2(seating);
            }

            return occupied.ToString();

            // var seating = GetSeatingArrangement();
            // bool stable = false;
            // int occupied = 0;
            // while (!stable)
            // {
            //     (seating, occupied, stable) = ProcessSeatingRound(seating, 4);
            // }

            // return occupied.ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        Tile[][] GetSeatingArrangement()
            => Input.SplitByNewline().Select(row => row.Select(c => new Tile { Seat = c == 'L' }).ToArray()).ToArray();

        Map<Tile> GetSeatingArrangementDict()
            => new Map<Tile>(GetSeatingArrangement());

        (Map<Tile>, int occupied, bool stable) ProcessSeatingRoundV2(Map<Tile> original, int preference)
        {
            var map = new Map<Tile>();

            int occupied = 0;
            bool stable = true;
            foreach (var key in original.Keys)
            {
                var tile = (Tile) original[key].Clone();
                map.Add(key, tile);
                if(!tile.Seat) continue;

                var adjacents = original.PeekAround(key, 1).Aggregate(0, (acc, direction) => 
                {
                    if (direction.Count() == 0) return acc;
                    return direction.First().Occupied ? acc + 1 : acc;
                });

                if (tile.Occupied)
                {
                    if (adjacents < preference)
                    {
                        occupied++;
                        continue;
                    }

                    stable = false;
                    tile.Occupied = false;
                }
                else if (adjacents == 0)
                {
                    tile.Occupied = true;
                    occupied++;
                    stable = false;
                }
            }

            return (map, occupied, stable);
        }

        (Tile[][] tiles, int occupied, bool stable) ProcessSeatingRound(Tile[][] tiles, int preference)
        {
            var original = tiles.Select(r => r.Select(t => (Tile)t.Clone()).ToArray()).ToArray();

            int occupied = 0;
            bool stable = true;
            int rows = tiles.Length;
            int cols = tiles[0].Length;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var seat = original[i][j];
                    if (!seat.Seat) continue;

                    if (seat.Occupied)
                    {
                        if (seat.AdjacentOccupied < preference)
                        {
                            occupied++;
                            continue;
                        }

                        stable = false;
                        tiles[i][j].Occupied = false;
                        foreach (var (x, y) in GetAdjacentTiles((i, j), rows, cols))
                        {
                            if (tiles[x][y].Seat) tiles[x][y].AdjacentOccupied--;
                        }
                    }
                    else if (seat.AdjacentOccupied == 0)
                    {
                        occupied++;
                        stable = false;
                        tiles[i][j].Occupied = true;
                        foreach (var (x, y) in GetAdjacentTiles((i, j), rows, cols))
                        {
                            if (tiles[x][y].Seat) tiles[x][y].AdjacentOccupied++;
                        }
                    }
                }
            }

            return (tiles, occupied, stable);
        }

        IEnumerable<(int x, int y)> GetAdjacentTiles((int x, int y) tile, int rows, int cols)
        {
            var (x, y) = tile;
            for (int i = x == 0 ? x : x - 1; i < rows && i < x + 2; i++)
            {
                for (int j = y == 0 ? y : y - 1; j >= 0 && j < cols && j < y + 2; j++)
                {
                    if (i == x && j == y) continue;
                    yield return (i, j);
                }
            }
        }

        void PrintSeatingArrangement(Tile[][] seats)
        {
            foreach (var x in seats)
            {
                foreach (var y in x)
                {
                    if (y.Occupied) Console.Write("#");
                    else if (y.Seat) Console.Write("L");
                    else Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        void PrintSeatingArrangementV2(Map<Tile> tiles)
        {
            string[][] output = new string[10][].Select(t => new string[10]).ToArray();

            foreach (var key in tiles.Keys)
            {
                output[key.x][key.y] = tiles[key].Occupied ? "#" : tiles[key].Seat ? "L" : ".";
            }

            foreach (var str in output) Console.WriteLine(str.JoinAsStrings());
            Console.WriteLine();
        }
    }

    internal class Tile : ICloneable
    {
        public bool Seat { get; set; }
        public bool Occupied { get; set; }
        public int AdjacentOccupied { get; set; }

        public object Clone() => new Tile
        {
            Seat = this.Seat,
            Occupied = this.Occupied,
            AdjacentOccupied = this.AdjacentOccupied,
        };
    }
}
