using System; 
using System.Collections.Generic; 
using System.Linq; 
using static AdventOfCode.Solutions.Utilities; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day10 : ASolution {

        bool[,] Map; 

        public Day10() : base(10, 2019, "Monitoring Station") {
            var lines = Input.SplitByNewline(); 
            Map = new bool[lines[0].Length, lines.Length]; 
            for(int x = 0; x < lines[0].Length; x++) {
                for(int y = 0; y < lines.Length; y++) {
                    Map[x,y] = lines[y][x] == '#';
                }
            }
        }

        protected override string SolvePartOne() {
            int count = 0; 
            for(int y = 0; y < Map.GetLength(1); y++) {
                for(int x = 0; x < Map.GetLength(0); x++) {
                    count = Math.Max(count, CountVisibleAsteroids((x,y))); 
                }
            }
            return count.ToString();
        }

        int CountVisibleAsteroids((int x, int y) asteroid) {
            var seen = new HashSet<(int x, int y)>();
            for(int y = 0; y < Map.GetLength(1); y++) {
                for(int x = 0; x < Map.GetLength(0); x++) {
                    bool hAsteroid = Map[x,y]; 
                    if(hAsteroid) {
                        int v = asteroid.y - y; 
                        int h = asteroid.x - x; 
                        if(h == 0 || v == 0) {
                            h = h < 0 ? -1 : h > 0 ? 1 : 0; 
                            v = v < 0 ? -1 : v > 0 ? 1 : 0; 
                        } else {
                            int GCD = FindGCD(Math.Abs(v), Math.Abs(h)); 
                            if(GCD > 1) {
                                v = v / GCD; 
                                h = h / GCD; 
                            }
                        }
                        seen.Add((v,h)); 
                    }
                }
            }
            return seen.Count() - 1; 
        }

        protected override string SolvePartTwo() {
            return null;
        }
    }
}
