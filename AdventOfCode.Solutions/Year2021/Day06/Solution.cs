using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021.Day06;

class Solution : SolutionBase
{
    public Solution() : base(06, 2021, "Lanternfish") { }

    protected override string SolvePartOne() =>
        SimulateFishForDays(Input.ToIntArray(","), 80).ToString();

    protected override string SolvePartTwo() =>
        SimulateFishForDays(Input.ToIntArray(","), 256).ToString();

    long SimulateFishForDays(IEnumerable<int> fish, int days)
    {
        var dict = fish.Aggregate(new Dictionary<int, long>(), (dict, age) =>
        {
            if (!dict.ContainsKey(age)) dict.Add(age, 0);
            dict[age]++;
            return dict;
        });

        for (var day = 1; day <= days; day++)
        {
            var newDict = new Dictionary<int, long>();

            for (var i = 0; i < 6; i++)
            {
                newDict.Add(i, dict.GetValueOrDefault(i + 1));
            }
            
            newDict.Add(6, dict.GetValueOrDefault(7) + dict.GetValueOrDefault(0));
            newDict.Add(7, dict.GetValueOrDefault(8));
            newDict.Add(8, dict.GetValueOrDefault(0));

            dict = newDict;
        }

        return dict.Values.Sum();
    }
}
