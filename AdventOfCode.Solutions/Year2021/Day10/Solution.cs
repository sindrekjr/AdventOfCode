using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2021.Day10;

class Solution : SolutionBase
{
    Dictionary<char, char> Endings = new Dictionary<char, char>()
    {
        ['('] = ')',
        ['['] = ']',
        ['{'] = '}',
        ['<'] = '>',
    };
    Dictionary<char, int> PointsForIllegals = new Dictionary<char, int>()
    {
        [')'] = 3,
        [']'] = 57,
        ['}'] = 1197,
        ['>'] = 25137,
    };
    Dictionary<char, int> PointsForCompletions = new Dictionary<char, int>()
    {
        ['('] = 1,
        ['['] = 2,
        ['{'] = 3,
        ['<'] = 4,
    };

    public Solution() : base(10, 2021, "Syntax Scoring")
    {

    }

    protected override string? SolvePartOne()
    {
        var lines = Input.SplitByNewline();

        return lines.Aggregate(0, (acc, line) =>
        {
            return acc + IsCorrupted(line);
        }).ToString();
    }

    protected override string? SolvePartTwo()
    {
        var incomplete = Input.SplitByNewline().Where(l => IsCorrupted(l) == 0);

        var scores = new List<long>();
        foreach (var line in incomplete)
        {
            var (stack, _) = StackLine(line);
            scores.Add(stack.Aggregate(0L, (acc, ch) => acc * 5 + PointsForCompletions[ch]));
        }

        return scores.OrderByDescending(s => s).ToArray()[scores.Count / 2].ToString();
    }        

    int IsCorrupted(string line)
    {
        var (_, illegal) = StackLine(line);
        return illegal == null ? 0 : PointsForIllegals[illegal.Value];
    }

    (Stack<char> stack, char? illegal) StackLine(string line)
    {
        var stack = new Stack<char>();

        foreach (var c in line)
        {
            if (c is '(' or '[' or '{' or '<')
            {
                stack.Push(c);
                continue;
            }

            if (Endings[stack.Peek()] == c)
            {
                stack.Pop();
                continue;
            }

            return (stack, c);
        }

        return (stack, null);
    }
}
