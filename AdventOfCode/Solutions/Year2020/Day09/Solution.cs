using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day09 : ASolution
    {

        public Day09() : base(09, 2020, "Encoding Error") { }

        protected override string SolvePartOne()
            => FindInvalidNumber(Input.ToIntArray("\n")).ToString();

        protected override string SolvePartTwo()
            => FindDecryptionWeakness(Input.ToIntArray("\n")).ToString();

        int FindInvalidNumber(int[] sequence, int preambleLength = 25)
        {
            for (int i = preambleLength;; i++)
            {
                int value = sequence[i];
                if (!IsValid(sequence.Skip(i - preambleLength).Take(preambleLength), value))
                {
                    return value;
                }
            }
        }

        int FindDecryptionWeakness(int[] sequence, int preambleLength = 25)
        {
            var targetSum = FindInvalidNumber(sequence, preambleLength);

            for (int end = 0, start = 0, sum = 0;; end++)
            {
                sum += sequence[end];
                while (sum > targetSum) sum -= sequence[start++];
                
                if (sum == targetSum)
                {
                    var subSequence = sequence[start..end];
                    return subSequence.Max() + subSequence.Min();
                }
            }
        }

        bool IsValid(IEnumerable<int> preamble, int sum)
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
