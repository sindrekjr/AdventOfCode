using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    class Day16 : ASolution
    {
        Dictionary<string, int[]> Rules;

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
            return null;
        }

        bool Validate(int n, int[] r)
            => (n >= r[0] && n <= r[1]) || (n >= r[2] && n <= r[3]);

        int[][] ParseInput()
        {
            Rules = new Dictionary<string, int[]>();
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
