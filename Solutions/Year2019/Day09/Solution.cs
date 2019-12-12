using System; 
using System.Collections.Generic;
using System.Linq; 
using System.Numerics; 

namespace AdventOfCode.Solutions.Year2019 {

    class Day09 : ASolution {

        IntcodeComputer<long> Machine; 

        public Day09() : base(9, 2019, "") {
            //DebugInput = "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99";
            //DebugInput = "1102,34915192,34915192,7,4,7,99,0";
            //DebugInput = "104,1125899906842624,99";
            Machine = new IntcodeComputer<long>(Input.ToLongArray(","));
        }

        protected override string SolvePartOne() {
            return Machine.Initialize().Run().Output.First().ToString();
        }

        protected override string SolvePartTwo() {
            return null;
        }
    }
}
