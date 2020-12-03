using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day03 : ASolution
    {
        bool[][] Map;

        public Day03() : base(03, 2020, "Toboggan Trajectory")
        {
            Map = Input.SplitByNewline().Select(line => line.Select(c => c == '#').ToArray()).ToArray();
        }

        protected override string SolvePartOne()
        {
            int x = 0;
            int count = 0;
            return CountSlopeTrees(1, 3).ToString();
            for (int y = 0; y < Map.Length; y++, x += 3)
            {
                if (x >= Map[y].Length) x -= Map[y].Length;
                if (Map[y][x]) count++;

                /*string line = "";
                foreach (var node in Map[y]) line += node ? "#" : ".";
                var sb = new StringBuilder(line);
                sb[x] = sb[x] == '#' ? 'X' : 'O';
                Console.WriteLine($"{sb.ToString()} -- {count}");*/
            }
            return count.ToString();
        }

        protected override string SolvePartTwo()
        {
            return (CountSlopeTrees(1, 1) * CountSlopeTrees(3, 1) * CountSlopeTrees(5, 1) * CountSlopeTrees(7, 1) * CountSlopeTrees(1, 2)).ToString();
        }

        BigInteger CountSlopeTrees(int down, int right)
        {
            int x = 0;
            int count = 0;
            for (int y = 0; y < Map.Length; y += down, x += right)
            {
                if (x >= Map[y].Length) x -= Map[y].Length;
                if (Map[y][x]) count++;
            }
            return count;
        }
    }
}
