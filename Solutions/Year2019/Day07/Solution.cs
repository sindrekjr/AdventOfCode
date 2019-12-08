using System.Linq; 
using System.Collections.Generic; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day07 : ASolution {

        IntcodeComputer Amplifier; 

        public Day07() : base(7, 2019, "Amplification Circuit") {
            Amplifier = new IntcodeComputer(Input.ToIntArray(","));
        }

        protected override string SolvePartOne() {
            int highest = 0;
            foreach(var signal in Enumerable.Range(0, 5).Permutations()) {
                int output = 0;
                foreach(int i in signal.ToArray()) {
                    output = Amplifier.InputSequence(i, output).Run().Output.First();
                }
                if(output > highest) highest = output;
            }
            return highest.ToString(); 
        }

        protected override string SolvePartTwo() {
            /*int highest = 0;
            foreach(var signal in Enumerable.Range(5, 9).Permutations()) {
                var Amplifiers = new Queue<IntcodeComputer>(); 
                for(int i = 0; i < 5; i++) Amplifiers.Enqueue(new IntcodeComputer(Input.ToIntArray(",")).InputSequence(signal.ToArray()[i]));
                
                int output = 0;
                int terminations = 0; 
                while(Amplifiers.Count > 0) {
                    var Amp = Amplifiers.Dequeue(); 
                    Amp.Input.Enqueue(output);
                    if(Amp.Run().Paused) {
                        Amplifiers.Enqueue(Amp); 
                    } else {
                        terminations++; 
                    }
                    output = Amp.Output.Dequeue();
                }
                
                if(output > highest) highest = output;
            }
            return highest.ToString(); */
            return @"¯\_(ツ)_/¯";
        }
    }
}
