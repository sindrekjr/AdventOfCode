using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day01 : ASolution
    {

        List<int> Expenses = new List<int>();

        public Day01() : base(01, 2020, "Report Repair")
        {
            foreach (var expense in Input.SplitByNewline()) 
            {
                Expenses.Add(int.Parse(expense));
            }
        }

        protected override string SolvePartOne()
        {
            foreach (var expense in Expenses)
            {
                if (expense > 2020) continue;
                foreach (var exp in Expenses)
                {
                    if (exp > 2020) continue;
                    if (expense + exp == 2020) return (expense * exp).ToString();
                }
            }
            return null;
        }

        protected override string SolvePartTwo()
        {
            foreach (var expense in Expenses)
            {
                if (expense > 2020) continue;
                foreach (var exp1 in Expenses)
                {
                    if (exp1 > 2020) continue;
                    foreach (var exp2 in Expenses)
                    {
                        if (expense + exp1 + exp2 == 2020) return (expense * exp1 * exp2).ToString();
                    }
                }
            }
            return null;
        }
    }
}
