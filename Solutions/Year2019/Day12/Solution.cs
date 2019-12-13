using System.Linq; 
using System.Collections.Generic; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day12 : ASolution {

        List<Moon> Moons; 
        HashSet<(Moon, Moon)> Pairs;

        public Day12() : base(12, 2019, "The N-Body Problem") {
            Initialize(); 
        }

        void Initialize() {
            var names = new Stack<string>(new string[]{"Callisto", "Ganymede", "Europa", "Io"}); 
            Moons = new List<Moon>();
            foreach(string position in Input.SplitByNewline()) {
                Moons.Add(new Moon(names.Pop(), position)); 
            }
            Pairs = FindPairs(); 
        }

        protected override string SolvePartOne() => TimeStep(1000).Select(m => m.GetPotentialEnergy() * m.GetKineticEnergy()).Sum().ToString();

        protected override string SolvePartTwo() {
            Initialize(); 
            return null;
        }

        List<Moon> TimeStep(int steps = 1) {
            for(int i = 1; i <= steps; i++) {
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
            return Moons; 
        }

        HashSet<(Moon, Moon)> FindPairs() {
            var pairs = new HashSet<(Moon, Moon)>(); 
            for(int i = 0; i < Moons.Count; i++) {
                for(int j = i + 1; j < Moons.Count; j++) {
                    pairs.Add((Moons[i], Moons[j])); 
                }
            }
            return pairs; 
        }
    }
}
