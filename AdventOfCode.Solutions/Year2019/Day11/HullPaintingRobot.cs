using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2019.Day11;

class HullPaintingRobot : XYRobot
{

    public Direction Facing { get; private set; }

    public HullPaintingRobot(IntcodeComputer comp) : base(comp) => Initialize();

    public HullPaintingRobot Initialize()
    {
        InitializeComputer(30000);
        Facing = Direction.Up;
        return this;
    }

    public Dictionary<(int, int), int> Run(int start = 0)
    {
        Map.Add(Position, start);
        do
        {
            Computer.WriteInput(Map.ContainsKey(Position) ? Map[Position] : 0).Run();
            Map[Position] = (int)Computer.Output.Dequeue();

            if(Computer.Output.Dequeue() == 0)
            {
                if(Facing == Direction.Up)
                {
                    Facing = Direction.Left;
                }
                else
                {
                    Facing--;
                }
            }
            else
            {
                if(Facing == Direction.Left)
                {
                    Facing = Direction.Up;
                }
                else
                {
                    Facing++;
                }
            }

            switch(Facing)
            {
                case Direction.Up:
                    MoveUp();
                    break;
                case Direction.Right:
                    MoveRight();
                    break;
                case Direction.Down:
                    MoveDown();
                    break;
                case Direction.Left:
                    MoveLeft();
                    break;
            }
        } while(Computer.Paused);
        return Map;
    }
}

public enum Direction { Up, Right, Down, Left }
