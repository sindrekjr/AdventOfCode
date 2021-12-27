using System;

namespace AdventOfCode.Solutions.Year2020.Day12;
public class Ship
{
    public (int x, int y) Position;
    public (int x, int y) Waypoint;
    public Direction Facing;

    public Ship((int, int) position, Direction facing = Direction.E)
    {
        Position = position;
        Facing = facing;
    }

    public Ship((int, int) position, (int, int) waypoint)
    {
        Position = position;
        Waypoint = waypoint;
    }

    public Ship DoActionByErroneousAssumptions(string action)
    {
        var (instruction, amount) = ParseAction(action);
        switch (instruction)
        {
            case "N":
                Move(Direction.N, amount, ref Position);
                break;

            case "S":
                Move(Direction.S, amount, ref Position);
                break;

            case "E":
                Move(Direction.E, amount, ref Position);
                break;

            case "W":
                Move(Direction.W, amount, ref Position);
                break;

            case "L":
                TurnLeft(amount);
                break;

            case "R":
                TurnRight(amount);
                break;
            
            case "F":
                Move(Facing, amount, ref Position);
                break;
        }
        
        return this;
    }

    public Ship DoAction(string action)
    {
        var (instruction, amount) = ParseAction(action);
        switch (instruction)
        {
            case "N":
                Move(Direction.N, amount, ref Waypoint);
                break;

            case "S":
                Move(Direction.S, amount, ref Waypoint);
                break;

            case "E":
                Move(Direction.E, amount, ref Waypoint);
                break;

            case "W":
                Move(Direction.W, amount, ref Waypoint);
                break;

            case "L":
                RotateWaypoint(amount, false);
                break;

            case "R":
                RotateWaypoint(amount, true);
                break;
            
            case "F":
                Position.x += Waypoint.x * amount;
                Position.y += Waypoint.y * amount;
                break;
        }

        return this;
    }

    (string instruction, int amount) ParseAction(string action)
    {
        var (instruction, amount, _) = action.SplitAtIndex(1);
        return (instruction, int.Parse(amount));
    }

    void Move(Direction facing, int distance, ref (int x, int y) coordinates)
    {
        switch(facing)
        {
            case Direction.N:
                coordinates.x -= distance;
                break;
            case Direction.S:
                coordinates.x += distance;
                break;
            case Direction.E:
                coordinates.y += distance;
                break;
            case Direction.W:
                coordinates.y -= distance;
                break;
        }
    }

    void TurnLeft(int degrees) 
    {
        for (int i = 0; i < degrees / 90; i++)
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
        for (int i = 0; i < degrees / 90; i++)
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

    void RotateWaypoint(int degrees, bool right)
    {
        for (int i = 0; i < degrees / 90; i++)
        {
            var (x, y) = Waypoint;
            Waypoint.x = right ? y : y * -1;
            Waypoint.y = right ? x * -1 : x;
        }
    }
}

public enum Direction { N, E, S, W }