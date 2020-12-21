using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day19 : ASolution
    {
        Dictionary<int, string[]> Rules;

        public Day19() : base(19, 2020, "Monster Messages") { }

        protected override string SolvePartOne()
        {
            var (rules, messages, _) = Input.SplitByParagraph();
            Rules = new Dictionary<int, string[]>();
            foreach (var line in rules.SplitByNewline())
            {
                var (key, value, _) = line.Split(": ");
                Rules.Add(int.Parse(key), value.Contains('|')
                    ? value.Split(" | ")
                    : new string[] { value.Trim('"') });
            }

            return messages.SplitByNewline().Count(m => Regex.IsMatch(m, $"^{ParseRule(0)}$")).ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        string ParseRule(int i)
        {
            var rule = Rules[i];

            if (rule.Length == 1 && rule[0].Length == 1 && !int.TryParse(rule[0], out int n))
            {
                return rule[0];
            }
            else
            {
                var alt = rule.Select(r => r.Split(" ").Select(n => ParseRule(int.Parse(n))).ToArray()).ToArray();
                return "(" + string.Join('|', alt.Select(n => n.JoinAsStrings())) + ")";
            }
        }

    }
}
