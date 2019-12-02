using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day01 : ASolution {
        
        public Day01() : base(1, 2019, "The Tyranny of the Rocket Equation") {
            
        }

        protected override string SolvePartOne() {
            return Input.ToIntArray("\n").Select(Fuel).Sum().ToString();
        }

        protected override string SolvePartTwo() {
            return Input.ToIntArray("\n").Select(FuelFuel).Sum().ToString();
        }
        
        int Fuel(int module) {
            return module / 3 - 2;
        }

        int FuelFuel(int module) {
            int fuel = Fuel(module); 
            return fuel <= 0 ? 0 : fuel + FuelFuel(fuel); 
        }
    }
}
