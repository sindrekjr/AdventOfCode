using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day01 : ASolution {
        
        public Day01() : base(1, 2019, "The Tyranny of the Rocket Equation") {
            
        }

        protected override string SolvePartOne() {
            return Input.ToIntArray("\n")
                .Select(mass => mass / 3 - 2)
                .Sum().ToString();
        }

        protected override string SolvePartTwo() {
            return Input.ToIntArray("\n")
                .Select(mass => FuelFuel(mass))
                .Sum().ToString();
        }

        int FuelFuel(int module) {
            int fuel = module / 3 - 2; 
            return fuel <= 0 ? 0 : fuel + FuelFuel(fuel); 
        }
    }
}
