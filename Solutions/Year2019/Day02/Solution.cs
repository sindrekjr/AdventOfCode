namespace AdventOfCode.Solutions.Year2019 {

    class Day02 : ASolution {

        IntcodeComputer Machine;

        public Day02() : base(2, 2019, "1202 Program Alarm") {
            Machine = new IntcodeComputer(Input.ToIntArray(","));
        }

        protected override string SolvePartOne() => Machine.Initialize(12, 2).Run().Memory[0].ToString();

        protected override string SolvePartTwo() {
            int noun = 0; 
            while(true) {
                int verb = 0;
                while(verb <= 100) {
                    Machine.Initialize(noun, verb);
                    if(Machine.Run().Memory[0] == 19690720) {
                        return (100 * noun + verb).ToString();
                    }
                    verb++;
                }
                noun++;
            }
        }
    }
}
