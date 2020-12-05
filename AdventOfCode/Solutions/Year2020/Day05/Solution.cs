using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day05 : ASolution
    {

        public Day05() : base(05, 2020, "Binary Boarding")
        {

        }

        protected override string SolvePartOne()
            => Input.SplitByNewline().Aggregate(0, (hi, bpass) => 
            {
                var (row, col) = GetSeat(bpass);
                var id = (row * 8) + col;
                return id > hi ? id : hi;
            }).ToString();

        protected override string SolvePartTwo()
        {
            return null;
        }

        (int row, int col) GetSeat(string bpass)
        {
            double front = 0;
            double back = 127;
            double left = 0;
            double right = 7;
            foreach (var ch in bpass)
            {
                if (ch == 'F') back -= Math.Ceiling((back - front) / 2);
                if (ch == 'B') front += Math.Ceiling((back - front) / 2);
                if (ch == 'L') right -= Math.Ceiling((right - left) / 2);
                if (ch == 'R') left += Math.Ceiling((right - left) / 2);
            }

            return ((int) front, (int) left);
        }
    }
}
