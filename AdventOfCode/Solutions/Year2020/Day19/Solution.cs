using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Day19 : ASolution
    {
        Dictionary<int, string[]> Rules;

        public Day19() : base(19, 2020, "Monster Messages", true) { }

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

            var rule0 = ParseRule(0);
            // Console.WriteLine(rule0);
            // return messages.SplitByNewline().Count(m => MatchStringRule(m, rule0)).ToString();

            return null;
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

        // bool MatchStringRule(string match, string rule)
        // {
        //     for (int i = 0, r = 0, parens = 0; i < match.Length; r++)
        //     {
        //         if (rule[r] == '|') continue;
        //         while (rule[r++] == '(') parens++;
        //         while (rule[r++] == ')') parens--;

        //         var c = rule[r];

        //         if (c == match[i]) 
        //         {
        //             i++;
        //         }
        //     }

        //     // int i = 0;
        //     // for (int j = 0; j < rule.Length; j++)
        //     // {
        //     //     var c = rule[j];
        //     //     if (c == '(')
        //     //     {
        //     //         if (rule[j + 1] == '(') continue;

        //     //         var end = Utilities.IndexOfClosingParenthesis(match, j);
        //     //         if (!MatchStringRule(match[i..], rule[(j + 1)..end])) return false;

        //     //         j = end - 1;
        //     //     }
        //     // }

        //     // return i == match.Length - 1;
        // }
    }
}
