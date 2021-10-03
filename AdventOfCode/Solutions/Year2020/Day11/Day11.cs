using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Day11 : ASolution
    {
        public Day11() : base(11, 2020, "Seating System") { }

        protected override string SolvePartOne()
        {
            var seating = GetSeatingArrangement();
            bool stable = false;
            int occupied = 0;
            while (!stable)
            {
                (seating, occupied, stable) = ProcessSeatingRound(seating, 4, false);
            }

            return occupied.ToString();
        }

        protected override string SolvePartTwo()
        {
            var seating = GetSeatingArrangement();
            bool stable = false;
            int occupied = 0;
            while (!stable)
            {
                (seating, occupied, stable) = ProcessSeatingRound(seating, 5);
            }

            return occupied.ToString();
        }

        Map<Tile> GetSeatingArrangement()
            => new Map<Tile>(
                Input.SplitByNewline()
                    .Select(row => row.Select(c => c == 'L' ? Tile.Seat : Tile.Empty).ToArray()).ToArray());

        (Map<Tile>, int occupied, bool stable) ProcessSeatingRound(Map<Tile> original, int preference, bool sight = true)
        {
            var map = new Map<Tile>();

            int occupied = 0;
            bool stable = true;
            foreach (var key in original.Keys)
            {
                var tile = original[key];
                map.Add(key, tile);
                if(tile == Tile.Empty) continue;

                var adjacents = original.PokeAround(key, sight ? -1 : 1).Aggregate(0, (acc, direction) => 
                {
                    foreach (var t in direction)
                    {
                        if (t == Tile.Occupied) return acc + 1;
                        if (t == Tile.Seat) return acc;
                    }

                    return acc;
                });

                if (tile == Tile.Occupied)
                {
                    if (adjacents < preference)
                    {
                        occupied++;
                        continue;
                    }

                    stable = false;
                    map[key] = Tile.Seat;
                }
                else if (adjacents == 0)
                {
                    map[key] = Tile.Occupied;
                    occupied++;
                    stable = false;
                }
            }

            return (map, occupied, stable);
        }
    }

    internal enum Tile { Empty, Seat, Occupied }
}
