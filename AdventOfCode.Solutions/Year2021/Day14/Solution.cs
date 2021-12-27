namespace AdventOfCode.Solutions.Year2021.Day14;

class Solution : SolutionBase
{
    public Solution() : base(14, 2021, "Extended Polymerization") { }

    protected override string SolvePartOne()
    {
        var (pairs, rules) = ParseInput();
        var last = pairs.Last().Key.Last();

        var counter = Step(pairs, rules, 10).Aggregate(new Dictionary<char, long>(), (dict, pair) =>
        {
            var (ch, _) = pair.Key.ToArray();
            if (!dict.ContainsKey(ch)) dict.Add(ch, 0);
            dict[ch] += pair.Value;
            return dict;
        });

        counter[last] += 1;

        return (counter.Values.Max() - counter.Values.Min()).ToString();
    }

    protected override string SolvePartTwo()
    {
        var (pairs, rules) = ParseInput();
        var last = pairs.Last().Key.Last();

        var counter = Step(pairs, rules, 40).Aggregate(new Dictionary<char, long>(), (dict, pair) =>
        {
            var (ch, _) = pair.Key.ToArray();
            if (!dict.ContainsKey(ch)) dict.Add(ch, 0);
            dict[ch] += pair.Value;
            return dict;
        });

        counter[last] += 1;

        return (counter.Values.Max() - counter.Values.Min()).ToString();
    }

    Dictionary<string, long> Step(Dictionary<string, long> pairs, Dictionary<string, char> rules, int steps) =>
        Enumerable.Range(0, steps).Aggregate(pairs, (pairs, _) => Step(pairs, rules));

    Dictionary<string, long> Step(Dictionary<string, long> pairs, Dictionary<string, char> rules)
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

    (Dictionary<string, long>, Dictionary<string, char>) ParseInput()
    {
        var (template, rules, _) = Input.SplitByParagraph();

        var pairs = new Dictionary<string, long>();
        for (int i = 0; i < template.Length - 1; i++)
        {
            var pair = $"{template[i]}{template[i + 1]}";
            if (!pairs.ContainsKey(pair)) pairs.Add(pair, 0);
            pairs[pair] += 1;
        }

        var rulesDict = new Dictionary<string, char>();
        foreach (var rule in rules.SplitByNewline())
        {
            var (couple, result, _) = rule.Split(" -> ");
            rulesDict.Add(couple, result.ToCharArray().First());
        }

        return (pairs, rulesDict);
    }
}
