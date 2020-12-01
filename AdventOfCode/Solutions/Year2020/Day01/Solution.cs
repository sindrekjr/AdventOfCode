using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day01 : ASolution
    {

        int[] Expenses;

        public Day01() : base(01, 2020, "Report Repair")
        {
            Expenses = Input.ToIntArray("\n").Where(e => e < 2020).ToArray();
        }

        protected override string SolvePartOne()
        {
            foreach (var e1 in Expenses)
            {
                foreach (var e2 in Expenses)
                {
                    if (e1 + e2 == 2020) return (e1 * e2).ToString();
                }
            }
            return null;
        }

        protected override string SolvePartTwo()
        {
            foreach (var e1 in Expenses)
            {
                foreach (var e2 in Expenses)
                {
                    foreach (var e3 in Expenses)
                    {
                        if (e1 + e2 + e3 == 2020) return (e1 * e2 * e3).ToString();
                    }
                }
            }
            return null;
        }
    }
}
