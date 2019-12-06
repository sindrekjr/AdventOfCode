using System.Collections.Generic; 
using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day06 : ASolution {

        public Day06() : base(6, 2019, "Universal Orbit Map") {

        }

        protected override string SolvePartOne() {
            var orbits = new Dictionary<string, (string direct, HashSet<string> indirect)>(); 
            foreach(string s in Input.SplitByNewline()) {
                string orbiter = s.Substring(s.IndexOf(")") + 1); 
                string orbitee = s.Substring(0, s.IndexOf(")")); 
                
                orbits.Add(orbiter, (orbitee, new HashSet<string>())); 
            }
            foreach(string orbiter in orbits.Keys) {
                var indirects = orbits[orbiter].indirect; 
                for(string o = orbiter; orbits.ContainsKey(o); o = orbits[o].direct) {
                    indirects.Add(o); 
                }
            }
            return orbits.Sum(x => x.Value.indirect.Count).ToString();
        }

        protected override string SolvePartTwo() {
            return null;
        }
    }
}
