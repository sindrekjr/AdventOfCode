using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day05 : ASolution
    {

        public Day05() : base(05, 2020, "Binary Boarding") { }

        protected override string SolvePartOne()
            => Input.SplitByNewline().Aggregate(0, (hi, bpass) =>
            {
                var id = GetSeatId(bpass);
                return id > hi ? id : hi;
            }).ToString();

        protected override string SolvePartTwo()
            => FindFreeSeat(Input.SplitByNewline().Select(GetSeatId).OrderBy(b => b).ToArray()).ToString();

        int GetSeatId(string bpass)
        {
            int front = 0;
            int back = 128;
            int left = 0;
            int right = 8;
            foreach (var ch in bpass)
            {                
                if (ch == 'F') back -= (back - front) / 2;
                else if (ch == 'B') front += (back - front) / 2;
                else if (ch == 'L') right -= (right - left) / 2;
                else if (ch == 'R') left += (right - left) / 2;
            }

            return (front * 8) + left;
        }

        int FindFreeSeat(int[] seats)
        {
            for (int i = 0;; i++) if (seats[i] + 1 != seats[i + 1]) return seats[i] + 1;
        }
    }
}
