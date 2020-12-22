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
            => Input.Replace(" ", "").SplitByNewline().Aggregate(default(long), (acc, exp) => acc + Resolve(exp, '+')).ToString();

        long Resolve(string expression, char precedence = default)
        {
            long sum = 0;
            char operand = default;
            List<long> storage = new List<long>();

            for (int i = 0; i < expression.Length; i++)
            {
                var c = expression[i];
                if (c == '(')
                {
                    var end = Utilities.IndexOfClosingParenthesis(expression, i) - 1;
                    var n = Resolve(expression[(i + 1)..(end)], precedence);
                    i = end;

                    if (operand == default)
                    {
                        sum = n;
                    }
                    else if (precedence != default && operand != precedence)
                    {
                        storage.Add(n);
                    }
                    else
                    {
                        sum = operand == '+' ? sum + n : sum * n;
                    }
                }
                else if (int.TryParse(c.ToString(), out int n))
                {
                    if (operand == default)
                    {
                        sum = n;
                    }
                    else if (precedence != default && operand != precedence)
                    {
                        expression = expression.Substring(0, i) + '(' + expression.Substring(i);
                        var end = expression.IndexOf(')', i);
                        expression = end == -1
                            ? expression + ')'
                            : expression.Substring(0, end) + ')' + expression.Substring(end);

                        i--;
                    }
                    else
                    {
                        sum = operand == '+' ? sum + n : sum * n;
                    }
                }
                else
                {
                    operand = c;
                }
            }

            return precedence == default
                ? sum
                : storage.Aggregate(sum, (acc, n) => precedence == '+' ? acc * n : acc + n);
        }
    }
}
