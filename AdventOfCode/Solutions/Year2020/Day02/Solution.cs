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
                if (isValid_Deprecated(pass[0], pass[1])) valid++;
            }
            return valid.ToString();
        }

        protected override string SolvePartTwo()
        {
            int valid = 0;
            foreach(var pass in Passwords)
            {
                if (isValid(pass[0], pass[1])) valid++;
            }
            return valid.ToString();
        }

        bool isValid_Deprecated(string policy, string password)
        {
            var (rule, pattern, _) = policy.Split(" ");
            var (lo, hi, _) = rule.Split("-");
            
            var count = (password.Length - password.Replace(pattern, "").Length) / pattern.Length;
            return count >= int.Parse(lo) && count <= int.Parse(hi);
        }

        bool isValid(string policy, string password)
        {
            var (rule, pattern, _) = policy.Split(" ");
            var (lo, hi, _) = rule.Split("-");

            int i = 0;
            int count = 0;
            while ((i = password.IndexOf(pattern, i)) != -1)
            {
                if ((i + 1) == int.Parse(lo) || (i + 1) == int.Parse(hi)) count++;
                i += pattern.Length;
            }

            return count == 1;
        }
    }
}
