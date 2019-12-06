using System; 
using System.Collections.Generic; 
using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day06 : ASolution {

        Dictionary<string, (string direct, List<string> indirect)> orbits; 

        public Day06() : base(6, 2019, "Universal Orbit Map") {

        }

        void InitializeCollection() {
            if(orbits != null) return; 
            orbits = new Dictionary<string, (string direct, List<string> indirect)>(); 
            foreach(string s in Input.SplitByNewline()) {
                string orbiter = s.Substring(s.IndexOf(")") + 1); 
                string orbitee = s.Substring(0, s.IndexOf(")")); 
                orbits.Add(orbiter, (orbitee, new List<string>())); 
            }
        }

        List<string> CollectIndirectOrbits(string orbiter) {
            var list = new List<string>(); 
            var set = orbits[orbiter].indirect; 
            for(string o = orbiter; orbits.ContainsKey(o); o = orbits[o].direct) {
                set.Add(o); 
                list.Add(o); 
            }
            return list; 
        }

        protected override string SolvePartOne() {
            InitializeCollection(); 
            foreach(string orbiter in orbits.Keys) {
                CollectIndirectOrbits(orbiter); 
            }
            return orbits.Sum(x => x.Value.indirect.Count).ToString();
        }

        protected override string SolvePartTwo() {
            InitializeCollection(); 
            var meList = CollectIndirectOrbits("YOU");
            var sanList = CollectIndirectOrbits("SAN"); 
            var both = new List<string>(meList.Concat(sanList));
            return (both.Count - ((both.Count - new HashSet<string>(both).Count + 1) * 2)).ToString();
        }
    }
}
