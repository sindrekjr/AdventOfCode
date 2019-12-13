using System; 
using System.Collections.Generic; 
using System.Numerics; 
using System.Text.RegularExpressions; 

namespace AdventOfCode.Solutions.Year2019 {

    class Moon {

        HashSet<(Vector3, Vector3)> History; 

        public string Name { get; private set; }
        public bool OhMyGodIHaveBeenHereBefore { get; private set; } 
        public Vector3 Position { get; private set; }
        public Vector3 Velocity { get; private set; }

        public Moon(string name, string position) {
            Name = name; 
            Position = ParsePosition(position); 
            Velocity = Vector3.Zero; 
            History = new HashSet<(Vector3, Vector3)>(){(Position, Velocity)}; 
        }

        public void UpdateVelocity((int x, int y, int z) gravity) {
            Velocity = new Vector3(
                Velocity.X + gravity.x,
                Velocity.Y + gravity.y,
                Velocity.Z + gravity.z
            );
        }

        public void ApplyVelocity() {
            Position = new Vector3(
                Position.X + Velocity.X, 
                Position.Y + Velocity.Y, 
                Position.Z + Velocity.Z
            ); 
            OhMyGodIHaveBeenHereBefore = !History.Add((Position, Velocity));
        }

        public int GetPotentialEnergy() => (int) (Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z));

        public int GetKineticEnergy() => (int) (Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z));

        Vector3 ParsePosition(string position) {
            var values = new Regex("(-?[0-9]+)").Matches(position); 
            return new Vector3(int.Parse(values[0].Value), int.Parse(values[1].Value), int.Parse(values[2].Value)); 
        }

        public override string ToString() => $"pos=<x={Position.X}, y={Position.Y}, z={Position.Z}>, vel=<x={Velocity.X}, y={Velocity.Y}, z={Velocity.Z}>"; 
    }
}