namespace AdventOfCode.Solutions.Year2019 {

    class Day09 : ASolution {

        IntcodeComputer<int> Machine; 

        public Day09() : base(9, 2019, "Sensor Boost") {
            Machine = new IntcodeComputer<int>(Input.ToIntArray(","));
        }

        protected override string SolvePartOne() => Machine.Initialize(1030).WriteInput(1).Run().Output.Dequeue().ToString();
        protected override string SolvePartTwo() => Machine.Initialize(1077).WriteInput(2).Run().Output.Dequeue().ToString();
    }
}
