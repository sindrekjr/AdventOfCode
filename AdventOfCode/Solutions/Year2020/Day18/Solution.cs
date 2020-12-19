using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day18 : ASolution
    {
        public Day18() : base(18, 2020, "Operation Order", true) { }

        protected override string SolvePartOne()
            => Input.Replace(" ", "").SplitByNewline().Aggregate(default(long), (acc, exp) => acc + Resolve(exp)).ToString();

        protected override string SolvePartTwo()
            => Input.Replace(" ", "").Replace("*", ")*(").SplitByNewline().Take(1).Aggregate(default(long), (acc, exp) => acc + Resolve($"({exp})")).ToString();

        long Resolve(string expression)
        {
            long sum = 0;
            char operand = default;
            for (int i = 0; i < expression.Length; i++)
            {
                var c = expression[i];
                if (c == '(')
                {
                    var end = IndexOfClosingParenthesis(expression, i) - 1;
                    var n = Resolve(expression[(i + 1)..(end)]);
                    i = end;
                    
                    sum = operand == default
                        ? n
                        : operand == '+' ? sum + n : sum * n;
                }
                else if (int.TryParse(c.ToString(), out int n))
                {
                    sum = operand == default
                        ? n
                        : operand == '+' ? sum + n : sum * n;
                }
                else
                {
                    operand = c;
                }
            }
            return sum;
        }

        int IndexOfClosingParenthesis(string expression, int i)
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
