using System.Collections.Generic; 

namespace AdventOfCode.Solutions.Year2019 {

    class HullPaintingRobot {

        IntcodeComputer Computer; 

        public Direction Facing { get; private set; }

        public HullPaintingRobot(IntcodeComputer comp) {
            Computer = comp; 
            Facing = Direction.Up; 
        }

        public Dictionary<(int, int), int> Run(int start = 0) {
            Computer.Initialize(1200); 

            (int x, int y) position = (0,0); 
            var Map = new Dictionary<(int x, int y), int>(); 

            Map.Add(position, start); 
            do {
                Computer.WriteInput(Map.ContainsKey(position) ? Map[position] : start).Run(); 
                Map[position] = (int) Computer.Output.Dequeue();

                if(Computer.Output.Dequeue() == 0) {
                    if(Facing == Direction.Up) {
                        Facing = Direction.Left; 
                    } else {
                        Facing--; 
                    }
                } else {
                    if(Facing == Direction.Left) {
                        Facing = Direction.Up; 
                    } else {
                        Facing++;
                    }
                }

                switch(Facing) {
                    case Direction.Up:
                        position.y++; 
                        break; 
                    case Direction.Right: 
                        position.x++; 
                        break; 
                    case Direction.Down: 
                        position.y--; 
                        break; 
                    case Direction.Left:
                        position.x--;
                        break; 
                }
            } while(Computer.Paused); 

            return Map; 
        }
    }

    public enum Direction { Up, Right, Down, Left }
}