using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{

    class Day07 : ASolution
    {
        Dictionary<string, List<string>> Rules;

        public Day07() : base(07, 2020, "Handy Haversacks") { }

        Dictionary<string, List<string>> GetBagRules()
        {
            var rules = new Dictionary<string, List<string>>();
            foreach (var line in Regex.Replace(Input, @"((bag[s]?)|([. 0-9]))", "").SplitByNewline())
            {
                var (container, contains, _) = line.Split("contain");
                var bags = contains.Split(",");
                foreach (var bag in bags)
                {
                    if (bag == "noother") continue;
                    if (!rules.ContainsKey(bag)) rules.Add(bag, new List<string>());
                    rules[bag].Add(container);
                }
            }
            return rules;
        }

        protected override string SolvePartOne()
        {
            Rules = GetBagRules();
            return CountPossibleContainers("shinygold").ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        int CountPossibleContainers(string colour)
            => Rules.ContainsKey(colour) ? Rules[colour].Count + Rules[colour].Aggregate(0, (count, bag) => count + CountPossibleContainers(bag)) : 1;
    }
}
