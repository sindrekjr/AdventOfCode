using System.Collections.Generic; 
using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class Arcade {

        IntcodeComputer Computer; 

        public int Score { get; private set; }
        public Dictionary<(int x, int y), int> Grid { get; private set; }

        public Arcade(IntcodeComputer comp) {
            Computer = comp; 
        }

        public Arcade Initialize() {
            Computer.Initialize(3000); 
            Grid = new Dictionary<(int x, int y), int>(); 
            return this; 
        }

        public Arcade Run(int quarters = 0) {
            if(quarters == 0) {
                Computer.Run(); 
                while(Computer.Output.Count > 0) Grid[((int) Computer.Output.Dequeue(), (int) Computer.Output.Dequeue())] = (int) Computer.Output.Dequeue();
            } else {
                Computer.SetMemory(0, quarters); 
                do {
                    Computer.Run(); 
                    while(Computer.Output.Count > 0) {
                        (int x, int y, int z) = ((int) Computer.Output.Dequeue(), (int) Computer.Output.Dequeue(), (int) Computer.Output.Dequeue()); 
                        if((-1, 0) == (x, y)) {
                            Score = z; 
                        } else {
                            Grid[(x, y)] = z; 
                        }
                    }
                    Computer.WriteInput(FindPaddle() > FindBall() ? -1 : FindPaddle() < FindBall() ? 1 : 0); 
                } while(Computer.Paused); 
            }
            return this; 
        }

        public int GetTileAmount(int tile) => Grid.Values.Where(t => t == tile).Count(); 

        int FindPaddle() => Grid.First(x => x.Value == 3).Key.x; 

        int FindBall() => Grid.First(x => x.Value == 4).Key.x; 
    }
}