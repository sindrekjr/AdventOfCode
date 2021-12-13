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

            var paper = dots.Select(dot =>
            {
                var (x, y, _) = dot.Split(",").Select(int.Parse).ToArray();
                return (x, y);
            }).ToHashSet();

            return Fold(paper, iAxis, param).Count.ToString();
        }

        protected override string SolvePartTwo()
        {
            var (dots, instructions, _) = Input.SplitByParagraph().Select(p => p.SplitByNewline()).ToArray();

            var paper = dots.Select(dot =>
            {
                var (x, y, _) = dot.Split(",").Select(int.Parse).ToArray();
                return (x, y);
            }).ToHashSet();

            foreach (var instr in instructions)
            {
                var (axis, param, _) = instr.Split("along ").ToArray()[1].Split("=");
                paper = Fold(paper, axis, int.Parse(param));
            }

            PaintPaper(paper, paper.Select(dot => dot.x).Max(), paper.Select(dot => dot.y).Max());

            return "Solved";
        }

        HashSet<(int x, int y)> Fold(HashSet<(int x, int y)> dots, string axis, int value) => dots.Select(dot =>
        {
            var (x, y) = dot;
            if (axis == "x" && x > value) return (value - (x - value), y);
            if (axis == "y" && y > value) return (x, value - (y - value));
            return (x, y);
        }).ToHashSet();

        void PaintPaper(HashSet<(int x, int y)> paper, int xEdge = 10, int yEdge = 14)
        {
            Console.WriteLine();
            for (int y = 0; y <= yEdge; y++)
            {
                for (int x = 0; x <= xEdge; x++)
                {
                    Console.Write(paper.Contains((x, y)) ? "#" : ".");
                }
                Console.WriteLine();
            }
        }
    }
}
