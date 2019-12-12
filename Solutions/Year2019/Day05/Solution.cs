namespace AdventOfCode.Solutions.Year2019 {

    class Day05 : ASolution {

        NewIntcodeComputer<int> Machine;

        public Day05() : base(5, 2019, "Sunny with a Chance of Asteroids") {
            //DebugInput = "3,3,1105,-1,9,1101,0,0,12,4,12,99,1";
            //DebugInput = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
            Machine = new NewIntcodeComputer<int>(Input.ToIntArray(","));
        }        

        protected override string SolvePartOne() => Machine.Initialize().WriteInput(1).Run().Diagnose().ToString();

        protected override string SolvePartTwo() {
            Machine.Initialize().WriteInput(5).Run(); 
            return Machine.Diagnose().ToString(); 
        }
    }
}
