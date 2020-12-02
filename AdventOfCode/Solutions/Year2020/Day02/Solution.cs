using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day02 : ASolution
    {
        string[][] Passwords;

        public Day02() : base(02, 2020, "Password Philosophy")
        {
            Passwords = Input.SplitByNewline().Select(str => str.Split(": ")).ToArray();
        }

        protected override string SolvePartOne()
        {
            int valid = 0;
            foreach(var pass in Passwords)
            {
                if (isValid(pass[0], pass[1])) valid++;
            }
            return valid.ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        bool isValid(string policy, string password)
        {
            var (rule, pattern, _) = policy.Split(" ");
            var (lo, hi, _) = rule.Split("-");
            
            var count = (password.Length - password.Replace(pattern, "").Length) / pattern.Length;
            return count >= int.Parse(lo) && count <= int.Parse(hi);
        }
    }
}
