using System; 
using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day07 : ASolution {

        IntcodeComputer Amplifier; 

        public Day07() : base(7, 2019, "Amplification Circuit") {
            Amplifier = new IntcodeComputer(Input.ToIntArray(","));
        }

        protected override string SolvePartOne() {
            int highest = 0;
            foreach(var signal in Enumerable.Range(0, 5).Permutations().ToList()) {
                int output = 0;
                foreach(int i in signal.ToArray()) {
                    output = Amplifier.InputSequence(i, output).Run().Output.First();
                }
                if (output > highest) highest = output;
            }
            return highest.ToString(); 
        }

        protected override string SolvePartTwo() {
            return null;
        }
    }
}
