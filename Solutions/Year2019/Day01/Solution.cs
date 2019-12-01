using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day01 : ASolution {
        
        public Day01() : base(1, 2019, "The Tyranny of the Rocket Equation") {
            
        }

        protected override string SolvePartOne() {
            return Input.SplitByNewline()
                .Select(mass => int.Parse(mass) / 3 - 2)
                .ToArray<int>()
                .Sum().ToString();
        }

        protected override string SolvePartTwo() {
            return Input.SplitByNewline()
                .Select(mass => FuelFuel(int.Parse(mass)))
                .ToArray<int>()
                .Sum().ToString();
        }

        int FuelFuel(int module) {
            int fuel = module / 3 - 2; 
            return fuel <= 0 ? 0 : fuel + FuelFuel(fuel); 
        }
    }
}
