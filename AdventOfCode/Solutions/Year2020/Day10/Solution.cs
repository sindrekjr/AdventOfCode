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
            => null;
        //     => GetCombinations(Input.ToIntArray("\n").OrderBy(n => n).ToArray(), new Dictionary<long, long>()).ToString();

        // long GetCombinations(int[] adapters, Dictionary<long, long> memo)
        // {
        //     if (memo.ContainsKey(adapters[0])) return memo[adapters[0]];

        //     long count = 0;
        //     for (int i = 1; i < adapters.Length; i++)
        //     {
        //         if (adapters[i] - adapters[0] > 3) break;
        //         count += GetCombinations(adapters[i..], memo);
        //     }

        //     if (count == 0) count = 1;

        //     memo.Add(adapters[0], count);
        //     return count;
        // }
    }
}
