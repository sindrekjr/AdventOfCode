namespace AdventOfCode.Solutions.Year2021
{
    class Day14 : ASolution
    {
        public Day14() : base(14, 2021, "Extended Polymerization") { }

        protected override string SolvePartOne()
        {
            var (template, rules) = ParseInput();

            for (int i = 0; i < 10; i++) template = Step(template.JoinAsStrings(), rules);

            var counter = new Dictionary<char, int>();
            foreach (var ch in template)
            {
                if (!counter.ContainsKey(ch)) counter.Add(ch, 0);
                counter[ch]++;
            }

            return (counter.Values.Max() - counter.Values.Min()).ToString();
        }

        protected override string SolvePartTwo()
        {
            var (template, rules) = ParseInput();

            var pairs = new Dictionary<string, long>();
            for (int i = 0; i < template.Length - 1; i++)
            {
                var pair = $"{template[i]}{template[i + 1]}";
                if (!pairs.ContainsKey(pair)) pairs.Add(pair, 0);
                pairs[pair] += 1;
            }

            for (int i = 0; i < 40; i++) pairs = NewStep(pairs, rules);

            var counter = pairs.Aggregate(new Dictionary<char, long>(), (dict, pair) =>
            {
                var (ch, _) = pair.Key.ToArray();
                if (!dict.ContainsKey(ch)) dict.Add(ch, 0);
                dict[ch] += pair.Value;
                return dict;
            });

            counter[template.Last()] += 1;

            return (counter.Values.Max() - counter.Values.Min()).ToString();
        }

        Dictionary<string, long> NewStep(Dictionary<string, long> pairs, Dictionary<string, char> rules)
        {
            var newPairs = new Dictionary<string, long>();

            foreach (var (pair, value) in pairs)
            {
                foreach (var newPair in GetResultingPairs(pair, rules))
                {
                    if (!newPairs.ContainsKey(newPair)) newPairs.Add(newPair, 0);
                    newPairs[newPair] += value;
                }
            }

            return newPairs;
        }

        string[] GetResultingPairs(string pair, Dictionary<string, char> rules) =>
            new[] { $"{pair[0]}{rules[pair]}", $"{rules[pair]}{pair[1]}" };

        string Step(string template, Dictionary<string, char> rules)
        {
            var newTemplate = "";

            for (int i = 0; i < template.Length; i++)
            {
                var current = template[i];
                newTemplate += current;

                if (i + 1 < template.Length)
                {
                    if (rules.TryGetValue($"{current}{template[i + 1]}", out char value)) newTemplate += value;
                }
            }

            return newTemplate;
        }

        (string, Dictionary<string, char>) ParseInput()
        {
            var (template, rules, _) = Input.SplitByParagraph();

            var dict = new Dictionary<string, char>();
            foreach (var rule in rules.SplitByNewline())
            {
                var (couple, result, _) = rule.Split(" -> ");
                dict.Add(couple, result.ToCharArray().First());
            }

            return (template, dict);
        }
    }

    internal class StringLengthComparer : IComparer<string>
    {
        public int Compare(string x, string y) => y.Length - x.Length;
    }
}
