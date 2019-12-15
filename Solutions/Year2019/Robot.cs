using System.Collections.Generic; 

namespace AdventOfCode.Solutions.Year2019 {

    partial class XYRobot {

        protected IntcodeComputer Computer; 
        protected (int x, int y) Position; 
        protected Dictionary<(int x, int y), int> Map;

        public XYRobot(
            IntcodeComputer comp, 
            (int x, int y)? pos = null, 
            Dictionary<(int, int), int> map = null) {
            Computer = comp; 
            Position = pos ?? (0, 0);
            Map = map ?? new Dictionary<(int, int), int>(); 
        }

        protected void InitializeComputer(int memory) => Computer.Initialize(memory); 

        protected void MoveUp() => Position.y++; 
        protected void MoveDown() => Position.y--; 
        protected void MoveRight() => Position.x++; 
        protected void MoveLeft() => Position.x--; 
    }
}