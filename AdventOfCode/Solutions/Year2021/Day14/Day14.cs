using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{
    class Day14 : ASolution
    {
        public Day14() : base(14, 2021, "Extended Polymerization") { }

        protected override string SolvePartOne()
        {
            var (template, rules) = ParseInputAsQueue();

            for (int i = 0; i < 10; i++) template = Step(template, rules);

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
            // var (template, rules) = ParseInputAsQueue();

            // for (int i = 0; i < 40; i++) template = Step(template, rules);

            // var counter = new Dictionary<char, long>();
            // foreach (var ch in template)
            // {
            //     if (!counter.ContainsKey(ch)) counter.Add(ch, 0);
            //     counter[ch]++;
            // }

            // return (counter.Values.Max() - counter.Values.Min()).ToString();

            return null;
        }

        Queue<char> Step(Queue<char> template, Dictionary<string, char> rules)
        {
            var newTemplate = new Queue<char>();
            while (template.Count != 0)
            {
                var current = template.Dequeue();
                newTemplate.Enqueue(current);

                if (template.TryPeek(out char next))
                {
                    newTemplate.Enqueue(rules[$"{current}{next}"]);
                }
            }

            return newTemplate;
        }

        (Queue<char>, Dictionary<string, char>) ParseInputAsQueue()
        {
            var (template, rules, _) = Input.SplitByParagraph();

            var queue = new Queue<char>();
            foreach (var ch in template) queue.Enqueue(ch);

            var dict = new Dictionary<string, char>();
            foreach (var rule in rules.SplitByNewline())
            {
                var (couple, result, _) = rule.Split(" -> ");
                dict.Add(couple, result.ToCharArray().First());
            }

            return (queue, dict);
        }
    }
}
