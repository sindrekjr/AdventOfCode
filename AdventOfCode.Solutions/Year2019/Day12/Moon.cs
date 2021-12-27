using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2019.Day12;

class Moon
{

    public string Name { get; private set; }
    public Vector3 Position { get; private set; }
    public Vector3 Velocity { get; private set; }

    public Moon(string name, string position)
    {
        Name = name;
        Position = ParsePosition(position);
        Velocity = Vector3.Zero;
    }

    public void UpdateVelocity(Vector3 gravity)
    {
        Velocity = Vector3.Add(Velocity, gravity);
    }

    public void ApplyVelocity()
    {
        Position = Vector3.Add(Position, Velocity);
    }

    public int GetPotentialEnergy() => (int)(Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z));

    public int GetKineticEnergy() => (int)(Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z));

    Vector3 ParsePosition(string position)
    {
        var values = new Regex("(-?[0-9]+)").Matches(position);
        return new Vector3(int.Parse(values[0].Value), int.Parse(values[1].Value), int.Parse(values[2].Value));
    }

    public override string ToString() => $"pos=<x={Position.X}, y={Position.Y}, z={Position.Z}>, vel=<x={Velocity.X}, y={Velocity.Y}, z={Velocity.Z}>";
}