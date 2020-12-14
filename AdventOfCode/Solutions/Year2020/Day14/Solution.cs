using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day14 : ASolution
    {

        public Day14() : base(14, 2020, "") { }

        protected override string SolvePartOne()
        {
            string bitmask = "";
            long[] memory = new long[99999];
            foreach (var instruction in GetProgram())
            {
                var (cmd, val, _) = instruction.Split(" = ");
                if (cmd == "mask")
                {
                    bitmask = val;
                }
                else
                {
                    var address = int.Parse(cmd.Substring(4, cmd.Length - 5));
                    var b = new BitArray(new int[] { int.Parse(val) });
                    for (int i = b.Length - 1, j = bitmask.Length - 1; i >= 0; i--, j--)
                    {
                        if (bitmask[j] == 'X') continue;
                        b[i] = bitmask[j] == '1';
                    }
                    memory[address] = b.ToLong();
                }
            }
            return memory.Sum().ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        string[] GetProgram() => Input.SplitByNewline();
    }
}
