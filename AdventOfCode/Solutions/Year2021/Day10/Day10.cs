using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{
    class Day10 : ASolution
    {
        public Day10() : base(10, 2021, "Syntax Scoring")
        {

        }

        protected override string SolvePartOne()
        {
            var lines = Input.SplitByNewline();

            return lines.Aggregate(0, (acc, line) =>
            {
                return acc + IsCorrupted(line);
            }).ToString();
        }

        protected override string SolvePartTwo()
        {
            var incomplete = Input.SplitByNewline().Where(l => IsCorrupted(l) == 0);
            return null;
        }        

        int IsCorrupted(string line)
        {
            var stack = new Stack<char>();
            var illegalChars = new Dictionary<char, int>()
            {
                [')'] = 3,
                [']'] = 57,
                ['}'] = 1197,
                ['>'] = 25137,
            };

            foreach (char c in line)
            {
                if (c is '(' or '[' or '{' or '<')
                {
                    stack.Push(c);
                    continue;
                }

                var opener = stack.Pop();

                if (c == ')' && opener == '(') continue;
                if (c == ']' && opener == '[') continue;
                if (c == '}' && opener == '{') continue;
                if (c == '>' && opener == '<') continue;

                return illegalChars[c];
            }

            return 0;
        }

        bool IsIncomplete(string line)
        {
            var openers = 0;
            var closers = 0;
            foreach (var c in line)
            {
                if (c is '(' or '[' or '{' or '<') openers++;
                if (c is ')' or ']' or '}' or '>') closers++;
            }

            Console.WriteLine($"o: {openers}; c: {closers}");

            return openers != closers;
        }
    }
}
