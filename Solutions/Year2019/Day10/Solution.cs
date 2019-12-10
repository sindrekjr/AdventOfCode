using System; 
using System.Collections.Generic; 
using static AdventOfCode.Solutions.Utilities; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day10 : ASolution {

        int size; 
        bool[,] Map; 
        (int x, int y) Station; 
        Dictionary<(int a, int b), (int x, int y)> Asteroids; 

        public Day10() : base(10, 2019, "Monitoring Station") {
            var lines = Input.SplitByNewline(); 

            size = lines.Length; 
            Map = new bool[size, size]; 
            for(int x = 0; x < size; x++) {
                for(int y = 0; y < size; y++) {
                    Map[x,y] = lines[y][x] == '#';
                }
            }
        }

        protected override string SolvePartOne() => DeployStation().ToString();

        protected override string SolvePartTwo() {
            if(Station == default((int,int))) DeployStation(); 

            return Station.x + ", " + Station.y;
        }

        int DeployStation() {
            int best = 0; 
            for(int x = 0; x < size; x++) {
                for(int y = 0; y < size; y++) {
                    if(Map[x, y]) {
                        var visible = FindVisibleAsteroids((x,y));
                        int count = visible.Count; 
                        if(count > best) {
                            best = count; 
                            Station = (x, y); 
                            Asteroids = visible; 
                        }
                    }
                }
            }
            return best; 
        }

        Dictionary<(int a, int b), (int x, int y)> FindVisibleAsteroids((int x, int y) asteroid) {
            var seen = new Dictionary<(int a, int b), (int x, int y)>();
            for(int x = 0; x < size; x++) {
                for(int y = 0; y < size; y++) {
                    if(!Map[x,y] || (x, y) == asteroid) continue; 
                    
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

                    if(seen.ContainsKey((v,h))) {
                        if(ManhattanDistance(asteroid, (x,y)) < ManhattanDistance(asteroid, seen[(v,h)])) {
                            seen[(v,h)] = (x,y); 
                        }
                    } else {
                        seen[(v,h)] = (x,y); 
                    }
                }
            }
            return seen;
        }

        (int x, int y) Find200thDestroyedAsteroid((int x, int y) station) {
            var MapOfVaporizedDestruction = (bool[,]) Map.Clone(); 
            int destroyed = 0; 
            while(true) {
                bool done = false; 
                for(int x = station.x; !done; x++) {
                    for(int y = 0; y < size; y++) {
                        bool hAsteroid = MapOfVaporizedDestruction[y,x]; 
                        if(hAsteroid) {
                            if(y == station.y && x == station.x) continue; 

                            int v = station.y - y; 
                            int h = station.x - x; 
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

                            if(++destroyed == 200) return (x,y);
                        }
                    }
                    done = x == station.x - 1; 
                    if(x + 1 == size) x = -1; 
                }
            }
        }
    }
}
