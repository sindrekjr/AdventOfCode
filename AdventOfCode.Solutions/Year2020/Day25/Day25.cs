using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020.Day25;

class Solution : SolutionBase
{
    int SubjectNumber = 7;

    public Solution() : base(25, 2020, "Combo Breaker") { }

    protected override string SolvePartOne()
    {
        var (cardkey, doorkey, _) = Input.ToIntArray("\n");
        return Transform(doorkey, FindLoopSize(cardkey)).ToString();
    }

    protected override string SolvePartTwo()
    {
        return null;
    }

    int FindLoopSize(int key)
    {
        for (int i = 1, val = SubjectNumber;; i++)
        {
            if (val == key) return i;
            val *= SubjectNumber;
            val %= 20201227;
        }
    }

    long Transform(long number, int loop)
    {
        for (long i = 1, subject = number; i < loop; i++)
        {
            number *= subject;
            number %= 20201227;
        }
        return number;
    }
}
