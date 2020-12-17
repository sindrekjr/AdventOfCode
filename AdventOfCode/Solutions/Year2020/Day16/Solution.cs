using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day16 : ASolution
    {
        SortedDictionary<string, int[]> Rules;

        public Day16() : base(16, 2020, "Ticket Translation") { }

        protected override string SolvePartOne()
            => ParseInput().Skip(1).Aggregate(0, (acc, ticket) => ticket.Aggregate(acc, (acc, val) => 
            {
                foreach (var rule in Rules.Values)
                {
                    if (Validate(val, rule)) return acc;
                }
                return acc + val;
            })).ToString();

        protected override string SolvePartTwo()
        {
            // var tickets = ParseInput().Where(IsValid).ToArray();
            // var candidates = tickets.Select(t => t.Select(GetCandidates).ToArray());
            // var eliminate = new IEnumerable<string>[tickets[0].Length];
            // for (int i = 0; i < eliminate.Length; i++) 
            // {
            //     eliminate[i] = candidates.Select(c => c[i]).IntersectAll();
            // }

            // foreach (var candidate in candidates)
            // {
            //     for (int i = 0; i < candidate.Length; i++)
            //     {
            //         candidate[i] = eliminate[i].Intersect(candidate[i]);
            //         foreach (var rule in Rules.Keys)
            //         {
            //             if (candidate[i].Contains(rule) && candidate.Count(c => c.Contains(rule)) == 1)
            //             {
            //                 candidate[i] = new string[] { rule };
            //                 break;
            //             }
            //         }
            //         Console.WriteLine(i + ": " + string.Join(", ", candidate[i]));
            //         eliminate[i] = candidate[i];
            //     }
            // }

            // long value = 1;
            // foreach (var e in eliminate)
            // {
            //     Console.WriteLine(string.Join(", ", e));
            // }

            return null;
        }

        IEnumerable<string> GetCandidates(int n)
            => Rules.Where(r => Validate(n, r.Value)).Select(r => r.Key);

        bool IsValid(int[] ticket)
            => ticket.All(val => Rules.Values.Any(r => Validate(val, r)));

        bool Validate(int n, int[] r)
            => (n >= r[0] && n <= r[1]) || (n >= r[2] && n <= r[3]);

        int[][] ParseInput()
        {
            Rules = new SortedDictionary<string, int[]>();
            var parts = Regex.Split(Input, "(\n\n[a-z ]+:\n)");

            foreach (var line in Regex.Replace(parts[0], @"(-)|( or )", ",").SplitByNewline())
            {
                var (key, rawValue, _) = line.Split(": ");
                Rules.Add(key, rawValue.ToIntArray(","));
            }

            return ParseTickets(parts[2], parts[4]).ToArray();
        }

        IEnumerable<int[]> ParseTickets(string myTicket, string nearbyTickets)
        {
            yield return myTicket.ToIntArray(",");
            foreach (var ticket in nearbyTickets.SplitByNewline())
            {
                yield return ticket.ToIntArray(",");
            }
        }
    }
}
