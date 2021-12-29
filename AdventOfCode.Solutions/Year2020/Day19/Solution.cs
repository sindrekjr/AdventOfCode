using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020.Day19;

class Solution : SolutionBase
{
    Dictionary<int, string[]> Rules = new();

    public Solution() : base(19, 2020, "Monster Messages") { }

    protected override string SolvePartOne()
    {
        var (rules, messages, _) = Input.SplitByParagraph();
        Rules = MapRulesToDictionary(rules.SplitByNewline());
        return messages.SplitByNewline().Count(m => Regex.IsMatch(m, $"^{ParseRule(0)}$")).ToString();
    }

    protected override string SolvePartTwo()
    {
        var (rules, messages, _) = Input.SplitByParagraph();
        
        Rules = MapRulesToDictionary(rules.SplitByNewline());
        Rules[8] = new string[] { "42", "42 8" };
        Rules[11] = new string[] { "42 31", "42 11 31" };

        return messages.SplitByNewline().Count(m => Regex.IsMatch(m, $"^{ParseRule(0)}$")).ToString();
    }

    Dictionary<int, string[]> MapRulesToDictionary(string[] rules)
    {
        var dict = new Dictionary<int, string[]>();
        foreach (var r in rules)
        {
            var (key, value, _) = r.Split(": ");
            dict.Add(int.Parse(key), value.Contains('|')
                ? value.Split(" | ")
                : new string[] { value.Trim('"') });
        }
        return dict;
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
            var alt = rule.Select(r => r.Split(" ").Select(n => ParseRule(int.Parse(n))));

            if (i == 8 && rule.Contains("42 8"))
            {
                alt = rule.Take(1).Select(r => r.Split(" ").Select(n => $"{ParseRule(int.Parse(n))}+"));
            }

            if (i == 11 && rule.Contains("42 11 31"))
            {
                alt = rule.Skip(1).Select(r => r.Split(" ").Select(n => 
                {
                    var r = int.Parse(n);
                    if (r == 42) return $"(?<el>{ParseRule(r)})+";
                    if (r == 11) return $"(?<th-el>{ParseRule(31)})+";
                    return "(?(el)(?!))";
                }));
            }
            
            return "(" + string.Join('|', alt.Select(n => n.JoinAsStrings())) + ")";
        }
    }
}
