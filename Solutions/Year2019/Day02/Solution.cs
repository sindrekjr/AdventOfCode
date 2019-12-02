using System;

namespace AdventOfCode.Solutions.Year2019 {

    class Day02 : ASolution {
        
        public Day02() : base(2, 2019, "1202 Program Alarm") {
            
        }

        protected override string SolvePartOne() {
            int[] code = Input.ToIntArray(",");
            code[1] = 12; 
            code[2] = 2; 
            for(int i = 0; i < code.Length; i++) {
                int output;
                if(code[i] == 1) {
                    output = code[code[++i]] + code[code[++i]];
                } else if(code[i] == 2) {
                    output = code[code[++i]] * code[code[++i]];
                } else if(code[i] == 99) {
                    break; 
                } else {
                    throw new SomethingWentWrongException(); 
                }
                code[code[++i]] = output; 
            }
            return code[0].ToString(); 
        }

        protected override string SolvePartTwo() {
            return null; 
        }

        class SomethingWentWrongException : Exception {

        }
    }
}
