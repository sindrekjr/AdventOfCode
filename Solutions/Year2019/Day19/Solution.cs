namespace AdventOfCode.Solutions.Year2019 {

    class Day19 : ASolution {

        IntcodeComputer Drone; 

        public Day19() : base(19, 2019, "") {
            Drone = new IntcodeComputer(Input.ToIntArray(",")); 
            //Drone.Initialize(5000); 
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
            return null;
        }
    }
}
