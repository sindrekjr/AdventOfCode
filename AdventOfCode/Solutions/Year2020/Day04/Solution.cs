using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{

    class Day04 : ASolution
    {
        Dictionary<string, string>[] Passports;
        string[] RequiredFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        string[] ValidHairColours = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

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
            => Passports.Count(ContainsRequiredFields).ToString();

        protected override string SolvePartTwo()
            => Passports.Where(ContainsRequiredFields).Count(Validate).ToString();

        bool ContainsRequiredFields(Dictionary<string, string> dict)
        {
            foreach (var field in RequiredFields)
            {
                if (!dict.ContainsKey(field)) return false;
            }
            return true;
        }

        bool Validate(Dictionary<string, string> dict)
        {
            try
            {
                var byr = int.Parse(dict["byr"]);
                if (byr < 1920 || 2002 < byr) return false;

                var iyr = int.Parse(dict["iyr"]);
                if (iyr < 2010 || 2020 < iyr) return false;

                var eyr = int.Parse(dict["eyr"]);
                if (eyr < 2020 || 2030 < eyr) return false;

                var hgt = dict["hgt"].SplitAtIndex(dict["hgt"].Length - 2);
                var hgtN = int.Parse(hgt[0]);
                var hgtM = hgt[1];
                if (hgtM == "cm") if (hgtN < 150 || 193 < hgtN) return false;
                if (hgtM == "in") if (hgtN < 59 || 76 < hgtN) return false;

                var hcl = dict["hcl"];
                if (!new Regex("#[a-z0-9]{6}").IsMatch(hcl)) return false;

                var ecl = dict["ecl"];
                if (!ValidHairColours.Contains(ecl)) return false;

                var pid = dict["pid"];
                if (pid.Length != 9 || !int.TryParse(pid, out int dummie)) return false;

                return true;
            }
            catch { return false; }
        }
    }
}
