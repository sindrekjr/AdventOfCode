using System.Collections.Generic; 
using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class Arcade {

        IntcodeComputer Computer; 
        public Dictionary<(int x, int y), int> Grid { get; private set; }

        public Arcade(IntcodeComputer comp) {
            Computer = comp; 
            Initialize();
        }

        public Arcade Initialize() {
            Grid = new Dictionary<(int x, int y), int>(); 
            return this; 
        }

        public Arcade Run() {
            Computer.Initialize(3000).Run(); 
            while(Computer.Output.Count > 0) {
                Grid[((int) Computer.Output.Dequeue(), (int) Computer.Output.Dequeue())] = (int) Computer.Output.Dequeue();
            }
            return this; 
        }

        public int GetTileAmount(int tile) => Grid.Values.Where(t => t == tile).Count(); 
    }
}