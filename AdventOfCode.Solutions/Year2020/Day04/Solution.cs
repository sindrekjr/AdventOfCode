using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020.Day04;

class Solution : SolutionBase
{
    string[] RequiredFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
    string[] ValidHairColours = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

    public Solution() : base(04, 2020, "Passport Processing") { }

    Dictionary<string, string>[] GetPassports() => Input.Split("\n\n").Select(pass => 
    {
        var dict = new Dictionary<string, string>();
        var fields = pass.Replace(" ", "\n").SplitByNewline();
        foreach (var field in fields)
        {
            var (k, v, _) = field.Split(":");
            dict.Add(k, v);
        }
        return dict;
    }).ToArray();

    protected override string? SolvePartOne()
        => GetPassports().Count(ContainsRequiredFields).ToString();

    protected override string? SolvePartTwo()
        => GetPassports().Where(ContainsRequiredFields).Count(Validate).ToString();

    bool ContainsRequiredFields(Dictionary<string, string> passport)
        => !RequiredFields.Any(field => !passport.ContainsKey(field));

    bool Validate(Dictionary<string, string> passport)
    {
        try
        {
            var byr = int.Parse(passport["byr"]);
            if (byr < 1920 || 2002 < byr) return false;

            var iyr = int.Parse(passport["iyr"]);
            if (iyr < 2010 || 2020 < iyr) return false;

            var eyr = int.Parse(passport["eyr"]);
            if (eyr < 2020 || 2030 < eyr) return false;

            var hgt = passport["hgt"].SplitAtIndex(passport["hgt"].Length - 2);
            var hgtN = int.Parse(hgt[0]);
            var hgtM = hgt[1];
            if (hgtM == "cm") if (hgtN < 150 || 193 < hgtN) return false;
            if (hgtM == "in") if (hgtN < 59 || 76 < hgtN) return false;

            var hcl = passport["hcl"];
            if (!new Regex("#[a-z0-9]{6}").IsMatch(hcl)) return false;

            var ecl = passport["ecl"];
            if (!ValidHairColours.Contains(ecl)) return false;

            var pid = passport["pid"];
            if (pid.Length != 9 || !int.TryParse(pid, out int dummie)) return false;

            return true;
        }
        catch { return false; }
    }
}
