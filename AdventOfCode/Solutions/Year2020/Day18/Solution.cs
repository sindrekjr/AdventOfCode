using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day18 : ASolution
    {
        public Day18() : base(18, 2020, "Operation Order") { }

        protected override string SolvePartOne()
            => Input.Replace(" ", "").SplitByNewline().Aggregate(default(long), (acc, exp) => acc + Resolve(exp)).ToString();

        protected override string SolvePartTwo()
        {
            return null;
        }

        long Resolve(string math)
        {
            long sum = 0;
            char operand = default;
            for (int i = 0; i < math.Length; i++)
            {
                if (math[i] == '(')
                {
                    var end = FindClose(math, i);
                    var n = Resolve(math[(i + 1)..(end - 1)]);
                    i = end - 1;
                    
                    if (operand == default)
                    {
                        sum = n;
                    }
                    else
                    {
                        sum = operand == '+' ? sum + n : sum * n;
                    }
                }
                else if (int.TryParse(math[i].ToString(), out int n))
                {
                    if (operand == default)
                    {
                        sum = n;
                    }
                    else
                    {
                        sum = operand == '+' ? sum + n : sum * n;
                    }
                }
                else
                {
                    operand = math[i];
                }

            }

            return sum;
        }

        int FindClose(string expression, int i)
        {
            int parens = 0;
            do
            {
                var c = expression[i++];
                if (c == '(') parens++;
                if (c == ')') parens--;
            } while (parens > 0);
            return i;
        }
    }
}
