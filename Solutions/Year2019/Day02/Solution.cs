using System.Linq;

namespace AdventOfCode.Solutions.Year2019 {

    class Day02 : ASolution {

        IntcodeComputer Machine;

        public Day02() : base(2, 2019, "1202 Program Alarm") {
            Machine = new IntcodeComputer(Input.ToIntArray(","));
        }

        protected override string SolvePartOne() {
            return Machine.Run(12, 2)[0].ToString();
        }

        protected override string SolvePartTwo() {
            foreach(int noun in Enumerable.Range(0, 99)) {
                foreach(int verb in Enumerable.Range(0, 99)) {
                    if(Machine.Run(noun, verb)[0] == 19690720) {
                        return (100 * noun + verb).ToString();
                    }
                }
            }
            return null;
        }
    }
}
