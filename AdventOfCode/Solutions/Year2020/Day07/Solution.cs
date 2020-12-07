using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{

    class Day07 : ASolution
    {
        public Day07() : base(07, 2020, "Handy Haversacks") { }

        protected override string SolvePartOne()
        {
            var rules = new Dictionary<string, List<string>>();
            foreach (var line in Regex.Replace(Input, $@"((bag[s]?)|([. 0-9]))", "").SplitByNewline())
            {
                var (container, contains, _) = line.Split("contain");
                var bags = contains.Split(",");
                foreach (var bag in bags)
                {
                    if (bag == "noother") continue;
                    if (!rules.ContainsKey(bag)) rules.Add(bag, new List<string>());
                    else if (rules[bag].Contains(container)) continue;
                    rules[bag].Add(container);
                }
            }
            return GetPossibleContainers(rules, "shinygold").Count.ToString();
        }

        protected override string SolvePartTwo()
        {
            var rules = new Dictionary<string, Dictionary<string, int>>();
            foreach (var line in Regex.Replace(Input, $@"((bag[s]?)|([. ]))", "").SplitByNewline())
            {
                var (container, contains, _) = line.Split("contain");
                if (contains.Contains("noother")) continue;

                var bags = contains.Split(",");
                var dictionary = new Dictionary<string, int>();
                foreach (var bag in bags)
                {
                    var (n, b, _) = bag.SplitAtIndex(1);
                    dictionary.Add(b, int.Parse(n));
                }
                rules.Add(container, dictionary);
            }
            return CountRequiredBags(rules, "shinygold").ToString();
        }

        HashSet<string> GetPossibleContainers(Dictionary<string, List<string>> rules, string colour)
        {
            var set = new HashSet<string>();
            if (rules.ContainsKey(colour))
            {
                set.UnionWith(rules[colour]);
                foreach (var rule in rules[colour])
                {
                    set.UnionWith(GetPossibleContainers(rules, rule));
                }
            }
            return set;
        }

        int CountRequiredBags(Dictionary<string, Dictionary<string, int>> rules, string colour)
        {
            int count = 0;
            if (rules.ContainsKey(colour))
            {
                foreach (var bag in rules[colour])
                {
                    count += bag.Value * (CountRequiredBags(rules, bag.Key) + 1);
                }
            }
            return count;
        }
    }
}
