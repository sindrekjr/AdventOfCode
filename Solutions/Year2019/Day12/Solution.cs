using System; 
using System.Linq; 
using System.Collections.Generic; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day12 : ASolution {

        List<Moon> Moons = new List<Moon>(); 
        HashSet<(Moon a, Moon b)> Pairs; 

        public Day12() : base(12, 2019, "The N-Body Problem") {
            var names = new Stack<string>(new string[]{"Callisto", "Ganymede", "Europa", "Io"}); 
            foreach(string position in Input.SplitByNewline()) {
                Moons.Add(new Moon(names.Pop(), position)); 
            }
            Pairs = FindPairs(); 
        }

        protected override string SolvePartOne() {
            for(int i = 0; i < 1000; i++) TimeStep(); 
            return Moons.Select(m => m.GetPotentialEnergy() * m.GetKineticEnergy()).Sum().ToString();
        }

        protected override string SolvePartTwo() {
            return null;
        }

        void TimeStep() {
            foreach((Moon, Moon) p in Pairs) {
                Moon A = p.Item1; 
                Moon B = p.Item2; 
                (int x, int y, int z) velA = (0, 0, 0); 
                (int x, int y, int z) velB = (0, 0, 0); 
                
                if(A.Position.x > B.Position.x) {
                    velA.x--; 
                    velB.x++; 
                } else if(A.Position.x < B.Position.x) {
                    velA.x++; 
                    velB.x--; 
                }

                if(A.Position.y > B.Position.y) {
                    velA.y--; 
                    velB.y++; 
                } else if(A.Position.y < B.Position.y) {
                    velA.y++; 
                    velB.y--; 
                }

                if(A.Position.z > B.Position.z) {
                    velA.z--; 
                    velB.z++; 
                } else if(A.Position.z < B.Position.z) {
                    velA.z++; 
                    velB.z--; 
                }

                A.UpdateVelocity(velA); 
                B.UpdateVelocity(velB); 
            }

            foreach(Moon M in Moons) M.ApplyVelocity(); 
        }

        HashSet<(Moon a, Moon b)> FindPairs() {
            var pairs = new HashSet<(Moon a, Moon b)>(); 
            for(int i = 0; i < Moons.Count; i++) {
                for(int j = i + 1; j < Moons.Count; j++) {
                    pairs.Add((Moons[i], Moons[j])); 
                }
            }
            return pairs; 
        }
    }
}
