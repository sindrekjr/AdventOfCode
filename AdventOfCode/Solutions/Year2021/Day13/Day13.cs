using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{
    class Day13 : ASolution
    {
        public Day13() : base(13, 2021, "Transparent Origami") { }

        protected override string SolvePartOne()
        {
            var (dots, instructions, _) = Input.SplitByParagraph().Select(p => p.SplitByNewline()).ToArray();
            var (iAxis, iParam, _) = instructions.First().Split("along ").ToArray()[1].Split("=");
            var param = int.Parse(iParam);

            var dotSet = dots.Select(dot =>
            {
                var (x, y, _) = dot.Split(",").Select(int.Parse).ToArray();
                return (x, y);
            }).ToHashSet();

            return Fold(dotSet, iAxis, param).Count.ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        HashSet<(int x, int y)> Fold(HashSet<(int x, int y)> dots, string axis, int value) => dots.Select(dot =>
        {
            var (x, y) = dot;
            if (axis == "x" && x > value) return (value - (x - value), y);
            if (axis == "y" && y > value) return (x, value - (y - value));
            return (x, y);
        }).ToHashSet();

        void PaintPaper(HashSet<(int x, int y)> paper)
        {
            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 11; x++)
                {
                    Console.Write(paper.Contains((x, y)) ? "#" : ".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
