namespace AdventOfCode.Solutions.Year2019 {

    class Day19 : ASolution {

        IntcodeComputer Drone; 

        public Day19() : base(19, 2019, "Tractor Beam") {
            Drone = new IntcodeComputer(Input.ToIntArray(",")); 
        }

        protected override string SolvePartOne() {
            int affected = 0; 
            for(int x = 0; x < 50; x++) {
                for(int y = 0; y < 50; y++) {
                    if(Drone.Initialize(500).WriteInput(x, y).Run().ReadOutput() == 1) affected++; 
                }
            }
            return affected.ToString();
        }

        protected override string SolvePartTwo() {
            int x = 0; 
            for(int y = 1000; ; y++) {
                while(Drone.Initialize(500).WriteInput(x, y).Run().ReadOutput() == 0) x++; 
                if(Drone.Initialize(500).WriteInput(x + 99, y - 99).Run().ReadOutput() == 1) {
                    return ((x * 10000) + (y - 99)).ToString(); 
                }
            }
        }
    }
}
