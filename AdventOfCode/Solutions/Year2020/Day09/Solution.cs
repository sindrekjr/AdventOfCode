using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day09 : ASolution
    {

        public Day09() : base(09, 2020, "Encoding Error")
        {

        }

        protected override string SolvePartOne()
        {
            var sequence = ParseXMAS();
            for (int i = 25; i < sequence.Length; i++)
            {
                int value = sequence[i];
                if (!IsSumOfPreamble(sequence.Skip(i - 25).Take(25), value))
                {
                    return value.ToString();
                }
            }
            return null;
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        int[] ParseXMAS() => Input.ToIntArray("\n");

        bool IsSumOfPreamble(IEnumerable<int> preamble, int sum)
        {
            var queue = new Queue<int>(preamble);
            while (queue.Any())
            {
                var n1 = queue.Dequeue();
                foreach (var n2 in queue)
                {
                    if (n1 + n2 == sum) return true;
                }
            }
            return false;
        }
    }
}
