using System; 
using System.Linq; 
using System.Collections.Generic; 
using System.Numerics; 
using static AdventOfCode.Solutions.Utilities; 

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
            return null; 
            //return FindFirstRepetitionOfSpaceHistory().ToString();
        }

        List<Moon> TimeStep(double steps = 1) {
            for(double i = 1; i <= steps; i++) {
                foreach((Moon, Moon) p in Pairs) {
                    Moon A = p.Item1; 
                    Moon B = p.Item2; 
                    var VelocityVectorA = new Vector3(0, 0, 0); 
                    var VelcoityVectorB = new Vector3(0, 0, 0); 
                    
                    if(A.Position.X > B.Position.X) {
                        VelocityVectorA.X--; 
                        VelcoityVectorB.X++; 
                    } else if(A.Position.X < B.Position.X) {
                        VelocityVectorA.X++; 
                        VelcoityVectorB.X--; 
                    }

                    if(A.Position.Y > B.Position.Y) {
                        VelocityVectorA.Y--; 
                        VelcoityVectorB.Y++; 
                    } else if(A.Position.Y < B.Position.Y) {
                        VelocityVectorA.Y++; 
                        VelcoityVectorB.Y--; 
                    }

                    if(A.Position.Z > B.Position.Z) {
                        VelocityVectorA.Z--; 
                        VelcoityVectorB.Z++; 
                    } else if(A.Position.Z < B.Position.Z) {
                        VelocityVectorA.Z++; 
                        VelcoityVectorB.Z--; 
                    }

                    A.UpdateVelocity(VelocityVectorA); 
                    B.UpdateVelocity(VelcoityVectorB); 
                }
                foreach(Moon M in Moons) M.ApplyVelocity();
            } 
            return Moons; 
        }

        double FindFirstRepetitionOfSpaceHistory() {
            (double x, double y, double z) = (0, 0, 0); 
            for(int i = 0; i < 3; i++) {
                Initialize(); 

                for(double j = 0; j < double.MaxValue; j++) {
                    TimeStep(); 

                    bool match = true; 
                    for(int k = 0; k < Moons.Count; k++) match &= Moons[k].Velocity == Vector3.Zero; 
                    if(match) {
                        if(i == 0) {
                            x = j + 1; 
                        } else if(i == 1) {
                            y = j + 1; 
                        } else if(i == 2) {
                            z = j + 1; 
                        }
                        break; 
                    }
                }
            }
            return FindLCM(x, FindLCM(y, z)) * 2; 
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
