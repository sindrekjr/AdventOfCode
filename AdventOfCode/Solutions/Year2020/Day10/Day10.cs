using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day10 : ASolution
    {

        public Day10() : base(10, 2020, "Adapter Array") { }

        protected override string SolvePartOne()
        {
            int count1 = 0;
            int count3 = 1;
            int previous = 0;
            foreach (var n in Input.ToIntArray("\n").OrderBy(n => n))
            {
                if (n - previous == 1) count1++;
                if (n - previous == 3) count3++;
                previous = n;
            }

            return (count1 * count3).ToString();
        }

        protected override string SolvePartTwo()
            => GetCombinations(Input.ToIntArray("\n").OrderBy(n => n).Prepend(0).ToArray(), new Dictionary<int, long>()).ToString();

        long GetCombinations(int[] adapters, Dictionary<int, long> memo)
        {
            if (memo.ContainsKey(adapters[0])) return memo[adapters[0]];

            long count = 0;
            for (int i = 1; i < adapters.Length && adapters[i] - adapters[0] <= 3; i++)
            {
                count += GetCombinations(adapters[i..], memo);
            }

            memo.Add(adapters[0], count == 0 ? 1 : count);
            return count == 0 ? 1 : count;
        }
    }
}
