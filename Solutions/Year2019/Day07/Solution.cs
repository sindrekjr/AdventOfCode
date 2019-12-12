using System.Linq; 
using System.Collections.Generic; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day07 : ASolution {

        NewIntcodeComputer<int> Amplifier; 

        public Day07() : base(7, 2019, "Amplification Circuit") {
            Amplifier = new NewIntcodeComputer<int>(Input.ToIntArray(","));
        }

        protected override string SolvePartOne() {
            int highest = 0;
            foreach(var signal in Enumerable.Range(0, 5).Permutations()) {
                int output = 0;
                foreach(int i in signal.ToArray()) {
                    output = (int) Amplifier.Initialize().WriteInput(i, output).Run().Output.First();
                }
                if(output > highest) highest = output;
            }
            return highest.ToString(); 
        }

        protected override string SolvePartTwo() {
            int highest = 0;
            foreach(var signal in Enumerable.Range(5, 5).Permutations()) {
                var Amplifiers = new Queue<NewIntcodeComputer<int>>(); 
                for(int i = 0; i < 5; i++) Amplifiers.Enqueue(new NewIntcodeComputer<int>(Input.ToIntArray(",")).WriteInput(signal.ToArray()[i]));
                
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
                    output = (int) Amp.Output.Dequeue();
                }
                
                if(output > highest) highest = output;
            }
            return highest.ToString(); 
        }
    }
}
