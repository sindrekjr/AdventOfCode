using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020.Day08;

class Solution : SolutionBase
{
    public Solution() : base(08, 2020, "Handheld Halting") { }

    protected override string SolvePartOne() => GetInstructions().Boot().acc.ToString();

    protected override string SolvePartTwo() => GetInstructions().TryFix().ToString();

    (string cmd, int n)[] GetInstructions() => Input.SplitByNewline().Select(l => 
    {
        var (inst, val, _) = l.Split(" ");
        return (inst, int.Parse(val.Contains("+") ? val.Substring(1) : val));
    }).ToArray();
}

static class Extensions
{
    public static (int acc, bool terminated) Boot(this (string cmd, int n)[] instructions)
    {
        int acc = 0;
        var visited = new bool[instructions.Length];
        for (int i = 0; i < instructions.Length;)
        {
            if (visited[i]) return (acc, false);

            visited[i] = true;

            if (instructions[i].cmd == "acc") acc += instructions[i++].n;
            else if (instructions[i].cmd == "jmp") i += instructions[i].n;
            else if (instructions[i].cmd == "nop") i++;
        }
        
        return (acc, true);
    }

    public static int TryFix(this (string cmd, int n)[] instructions, int i = 0)
    {
        if (instructions[i].cmd == "jmp")
        {
            instructions[i].cmd = "nop";
            var (acc, success) = instructions.Boot();
            
            if (success)
            {
                return acc;
            }
            else
            {
                instructions[i].cmd = "jmp";
                return instructions.TryFix(i + 1);
            }
        }
        else if (instructions[i].cmd == "nop")
        {
            instructions[i].cmd = "jmp";
            var (acc, success) = instructions.Boot();
            
            if (success)
            {
                return acc;
            }
            else
            {
                instructions[i].cmd = "nop";
                return instructions.TryFix(i + 1);
            }
        }

        return instructions.TryFix(i + 1);
    }
}
