using System;

namespace AdventOfCode.Solutions.Year2020
{
    public class Ship
    {
        public (int x, int y) Position;
        public Direction Facing;

        public Ship((int, int) position, Direction facing = Direction.E)
        {
            Position = position;
            Facing = facing;
        }

        public void ParseAction(string action)
        {
            var (instruction, stramnt, _) = action.SplitAtIndex(1);
            var amount = int.Parse(stramnt);
            
            switch (instruction)
            {
                case "N":
                    MoveNorth(amount);
                    break;

                case "S":
                    MoveSouth(amount);
                    break;

                case "E":
                    MoveEast(amount);
                    break;

                case "W":
                    MoveWest(amount);
                    break;

                case "L":
                    TurnLeft(amount);
                    break;

                case "R":
                    TurnRight(amount);
                    break;
                
                case "F":
                    Move(Facing, amount);
                    break;
            }
        }

        void Move(Direction facing, int distance)
        {
            switch(facing)
            {
                case Direction.N:
                    MoveNorth(distance);
                    break;
                case Direction.S:
                    MoveSouth(distance);
                    break;
                case Direction.E:
                    MoveEast(distance);
                    break;
                case Direction.W:
                    MoveWest(distance);
                    break;
            }
        }

        void MoveNorth(int distance) => Position.y += distance;
        void MoveSouth(int distance) => Position.y -= distance;
        void MoveEast(int distance) => Position.x += distance;
        void MoveWest(int distance) => Position.x -= distance;

        void TurnLeft(int degrees) 
        {
            var turns = degrees / 90;
            for (int i = 0; i < turns; i++)
            {
                if (Facing == Direction.N)
                {
                    Facing = Direction.W;
                }
                else
                {
                    Facing--;
                }
            }
        }

        void TurnRight(int degrees)
        {
            var turns = degrees / 90;
            for (int i = 0; i < turns; i++)
            {
                if (Facing == Direction.W)
                {
                    Facing = Direction.N;
                }
                else
                {
                    Facing++;
                }
            }
        }
    }

    public enum Direction { N, E, S, W }
}