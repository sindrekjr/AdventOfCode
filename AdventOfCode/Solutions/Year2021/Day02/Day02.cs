using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{
    class Day02 : ASolution
    {
        public Day02() : base(02, 2021, "Dive!")
        {
        }

        protected override string SolvePartOne()
        {
            var x = 0;
            var y = 0;

            foreach (var instruction in Input.SplitByNewline())
            {
                var (cmd, amt, _) = instruction.Split(" ");

                if (cmd == "forward") x += int.Parse(amt);
                if (cmd == "up") y -= int.Parse(amt);
                if (cmd == "down") y += int.Parse(amt);
            }
            return (x * y).ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }
    }
}