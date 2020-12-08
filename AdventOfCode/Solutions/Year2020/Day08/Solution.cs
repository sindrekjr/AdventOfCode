using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day08 : ASolution
    {

        public Day08() : base(08, 2020, "Handheld Halting")
        {

        }

        (string cmd, int n, bool visited)[] GetInstructions() => Input.SplitByNewline().Select(l => 
        {
            var (inst, val, _) = l.Split(" ");
            return (inst, int.Parse(val.Contains("+") ? val.Substring(1) : val), false);
        }).ToArray();

        protected override string SolvePartOne()
        {
            int acc = 0;
            var instructions = GetInstructions();
            for (int i = 0;;)
            {
                if (instructions[i].visited) return acc.ToString();

                instructions[i].visited = true;
                if (instructions[i].cmd == "acc") acc += instructions[i++].n;
                if (instructions[i].cmd == "jmp") i += instructions[i].n;
                if (instructions[i].cmd == "nop") i++;
            }
        }

        protected override string SolvePartTwo()
        {
            return null;
        }
    }
}
