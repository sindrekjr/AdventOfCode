using System; 

namespace AdventOfCode.Solutions.Year2019 {
    class IntcodeComputer {

        readonly int[] intcode; 

        public IntcodeComputer(int[] intcode) {
            this.intcode = intcode; 
        }

        public int[] Run(Nullable<int> noun = null, Nullable<int> verb = null) {
            int[] code = new int[intcode.Length]; 
            intcode.CopyTo(code, 0);
            if(noun != null) code[1] = noun.Value; 
            if(verb != null) code[2] = verb.Value; 
            for(int i = 0; i < code.Length; i++) {
                int opcode = GetOpcode(code[i]); 
                int output;
                if(opcode == 1) {
                    output = code[code[++i]] + code[code[++i]];
                } else if(opcode == 2) {
                    output = code[code[++i]] * code[code[++i]];
                } else if(opcode == 99) {
                    break;
                } else {
                    throw new SomethingWentWrongException();
                }
                code[code[++i]] = output;
            }
            return code;
        }

        int GetOpcode(int instruction) {
            if(instruction < 100) return instruction; 

            return instruction; 
        }
    }

    class SomethingWentWrongException : Exception {

    }
}