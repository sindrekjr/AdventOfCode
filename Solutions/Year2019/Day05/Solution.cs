namespace AdventOfCode.Solutions.Year2019 {

    class Day05 : ASolution {

        IntcodeComputer Machine;

        public Day05() : base(5, 2019, "Sunny with a Chance of Asteroids") {
            Machine = new IntcodeComputer(Input.ToIntArray(","));
        }        

        protected override string SolvePartOne() {
            Machine.Run(1); 
            return Machine.Diagnose().ToString(); 
        }

        protected override string SolvePartTwo() {
            Machine.Run(5); 
            return Machine.Diagnose().ToString(); 
        }
    }
}
