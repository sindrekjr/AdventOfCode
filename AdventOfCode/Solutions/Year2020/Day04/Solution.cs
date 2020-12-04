using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day04 : ASolution
    {
        Dictionary<string, string>[] Passports;

        public Day04() : base(04, 2020, "Passport Processing")
        {
            Passports = Input.Split("\n\n").Select(pass => {
                var dict = new Dictionary<string, string>();
                var fields = pass.Replace(" ", "\n").SplitByNewline();
                foreach (var field in fields)
                {
                    var (k, v, _) = field.Split(":");
                    dict.Add(k, v);
                }
                return dict;
            }).ToArray();
        }

        protected override string SolvePartOne()
        {
            var requiredFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            int count = 0;
            foreach (var passport in Passports)
            {
                if (ContainsAll(passport, requiredFields)) count++;
            }
            
            return count.ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        bool ContainsAll(Dictionary<string, string> dict, string[] fields)
        {
            foreach (var field in fields)
            {
                if (!dict.ContainsKey(field)) return false;
            }
            return true;
        }
    }
}
