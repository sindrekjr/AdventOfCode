using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Solutions.Year2020.Day03;

class Solution : SolutionBase
{
    bool[][] Map;

    public Solution() : base(03, 2020, "Toboggan Trajectory")
        => Map = Input.SplitByNewline().Select(line => line.Select(c => c == '#').ToArray()).ToArray();

    protected override string SolvePartOne() => CountSlopeTrees(1, 3).ToString();

    protected override string SolvePartTwo()
        => (CountSlopeTrees(1, 1) * CountSlopeTrees(1, 3) * CountSlopeTrees(1, 5) * CountSlopeTrees(1, 7) * CountSlopeTrees(2, 1)).ToString();

    long CountSlopeTrees(int down, int right)
    {
        int count = 0;
        for (int x = 0, y = 0; y < Map.Length; y += down, x += right)
        {
            x %= Map[y].Length;
            if (Map[y][x]) count++;
        }
        return count;
    }
}
