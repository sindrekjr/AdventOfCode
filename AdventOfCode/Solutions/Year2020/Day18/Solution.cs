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
            => Input.Replace(" ", "").SplitByNewline().Aggregate(default(long), (acc, exp) => acc + Resolve(exp, '+')).ToString();

        long Resolve(string expression, char precedence = default)
        {
            var values = new Stack<long>();
            var operands = new Stack<char>();

            operands.Push('(');

            foreach (var c in expression)
            {
                if (c == '(') operands.Push(c);
                else if (c == ')')
                {
                    while (operands.Peek() != '(')
                    {
                        values.Push(operands.Pop() == '+'
                            ? values.Pop() + values.Pop()
                            : values.Pop() * values.Pop());
                    }
                    operands.Pop();
                }
                else if (long.TryParse(c.ToString(), out long n)) values.Push(n);
                else
                {
                    while (operands.Peek() != '(' && (precedence == default || operands.Peek() == precedence))
                    {
                        values.Push(operands.Pop() == '+'
                            ? values.Pop() + values.Pop()
                            : values.Pop() * values.Pop());
                    }
                    operands.Push(c);
                }
            }

            while (operands.Peek() != '(')
            {
                values.Push(operands.Pop() == '+'
                    ? values.Pop() + values.Pop()
                    : values.Pop() * values.Pop());
            }

            return values.Single();
        }
    }
}
