using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Day23 : ASolution
    {
        public Day23() : base(23, 2020, "Crab Cups") { }

        protected override string SolvePartOne()
            => ProcessMoves(Input.ToIntArray().ToList(), 100).JoinAsStrings();

        protected override string SolvePartTwo()
        {
            return null;
        }

        List<int> ProcessMoves(List<int> cups, int amount)
        {
            var current = 0;
            for (int c = 0, currentCup = cups[current]; c < amount; c++, currentCup = cups[current])
            {
                // Console.WriteLine("-- move " + (c + 1) + " --");
                // Console.WriteLine("cups: " + cups.JoinAsStrings(" "));

                var picked = new List<int>();
                while (picked.Count < 3)
                {
                    picked.Add(NextClockwise(cups, current));
                }

                // Console.WriteLine("pick up: " + picked.JoinAsStrings(" "));

                var destination = currentCup- 1;
                while (!cups.Contains(destination))
                {
                    destination = destination < cups.Min()
                        ? cups.Max()
                        : destination - 1;
                }

                // Console.WriteLine("destination: " + destination);

                for (int i = 2, dest = cups.IndexOf(destination) + 1; i >= 0; i--) cups.Insert(dest, picked[i]);
                
                current = cups.IndexOf(currentCup) + 1;
                if (current >= cups.Count) current = 0;

                // Console.WriteLine();
            }

            return cups;
        }

        int NextClockwise(List<int> cups, int i)
        {
            i = ++i < cups.Count ? i : 0;
            var next = cups[i];
            cups.RemoveAt(i);
            return next;
        }
    }
}
