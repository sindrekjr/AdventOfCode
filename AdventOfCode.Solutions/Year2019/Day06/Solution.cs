using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019.Day06;

class Solution : SolutionBase
{

    Dictionary<string, (string direct, List<string> indirect)> orbits = new();

    public Solution() : base(6, 2019, "Universal Orbit Map") { }

    void InitializeCollection()
    {
        orbits = new Dictionary<string, (string direct, List<string> indirect)>();
        foreach(string s in Input.SplitByNewline())
        {
            string orbiter = s.Substring(s.IndexOf(")") + 1);
            string orbitee = s.Substring(0, s.IndexOf(")"));
            orbits.Add(orbiter, (orbitee, new List<string>()));
        }
    }

    List<string> CollectIndirectOrbits(string orbiter)
    {
        var list = new List<string>();
        var set = orbits[orbiter].indirect;
        for(string o = orbiter; orbits.ContainsKey(o); o = orbits[o].direct)
        {
            set.Add(o);
            list.Add(o);
        }
        return list;
    }

    protected override string SolvePartOne()
    {
        InitializeCollection();
        foreach(string orbiter in orbits.Keys)
        {
            CollectIndirectOrbits(orbiter);
        }
        return orbits.Sum(x => x.Value.indirect.Count).ToString();
    }

    protected override string SolvePartTwo()
    {
        InitializeCollection();
        var meAndSanta = new List<string>(CollectIndirectOrbits("YOU").Concat(CollectIndirectOrbits("SAN")));
        return (meAndSanta.Count - ((meAndSanta.Count - new HashSet<string>(meAndSanta).Count + 1) * 2)).ToString();
    }
}
