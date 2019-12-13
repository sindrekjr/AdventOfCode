namespace AdventOfCode.Solutions.Year2019 {

    class Day13 : ASolution {

        Arcade Arcade; 

        public Day13() : base(13, 2019, "Care Package") {
            Arcade = new Arcade(new IntcodeComputer(Input.ToIntArray(","))); 
        }

        protected override string SolvePartOne() {
            return Arcade.Run().GetTileAmount(2).ToString();
        }

        protected override string SolvePartTwo() {
            return null;
        }
    }
}
