using System; 
using System.Text.RegularExpressions; 

namespace AdventOfCode.Solutions.Year2019 {

    class Moon {

        public string Name { get; private set; }
        public (int x, int y, int z) Position { get; private set; }
        public (int x, int y, int z) Velocity { get; private set; }

        public Moon(string name, string position) {
            this.Name = name; 
            this.Position = ParsePosition(position); 
            this.Velocity = (0, 0, 0); 
        }

        public void UpdateVelocity((int x, int y, int z) gravity) {
            Velocity = (
                Velocity.x + gravity.x,
                Velocity.y + gravity.y,
                Velocity.z + gravity.z
            );
        }

        public void ApplyVelocity() {
            Position = (
                Position.x + Velocity.x, 
                Position.y + Velocity.y, 
                Position.z + Velocity.z
            ); 
        }

        public int GetPotentialEnergy() => Math.Abs(Position.x) + Math.Abs(Position.y) + Math.Abs(Position.z);

        public int GetKineticEnergy() => Math.Abs(Velocity.x) + Math.Abs(Velocity.y) + Math.Abs(Velocity.z);

        (int, int, int) ParsePosition(string position) {
            var values = new Regex("(-?[0-9]+)").Matches(position); 
            return (int.Parse(values[0].Value), int.Parse(values[1].Value), int.Parse(values[2].Value)); 
        }

        public override string ToString() => $"pos=<x={Position.x}, y={Position.y}, z={Position.z}>, vel=<x={Velocity.x}, y={Velocity.y}, z={Velocity.z}>"; 
    }
}