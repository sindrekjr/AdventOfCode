using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020.Day02;

class Solution : SolutionBase
{
    (int, int, string) Policy;

    public Solution() : base(02, 2020, "Password Philosophy") { }

    protected override string? SolvePartOne()
        => Input.SplitByNewline().Count(line => IsValid_Deprecated(ParseLine(line))).ToString();

    protected override string? SolvePartTwo()
        => Input.SplitByNewline().Count(line => IsValid(ParseLine(line))).ToString();

    string ParseLine(string line)
    {
        var (policy, password, _) = line.Split(": ");
        Policy = ParsePolicy(policy);
        return password;
    }

    (int, int, string) ParsePolicy(string policy)
    {
        var (numbers, pattern, _) = policy.Split(" ");
        var (first, second, _) = numbers.Split("-");
        return (int.Parse(first), int.Parse(second), pattern);
    }

    bool IsValid_Deprecated(string password)
    {
        var (min, max, pattern) = Policy;
        var count = (password.Length - password.Replace(pattern, "").Length) / pattern.Length;
        return count >= min && count <= max;
    }

    bool IsValid(string password)
    {
        int i = 0;
        int count = 0;
        var (first, second, pattern) = Policy;
        while ((i = password.IndexOf(pattern, i)) != -1)
        {
            if ((i + 1) == first || (i + 1) == second) count++;
            i += pattern.Length;
        }

        return count == 1;
    }
}
