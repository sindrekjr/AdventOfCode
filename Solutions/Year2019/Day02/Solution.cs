using System;
using System.Linq;

namespace AdventOfCode.Solutions.Year2019 {

    class Day02 : ASolution {

        public Day02() : base(2, 2019, "1202 Program Alarm") {

        }

        protected override string SolvePartOne() {
            return Run(Input.ToIntArray(","), 12, 2)[0].ToString();
        }

        protected override string SolvePartTwo() {
            foreach(int noun in Enumerable.Range(0, 99)) {
                foreach(int verb in Enumerable.Range(0, 99)) {
                    if(Run(Input.ToIntArray(","), noun, verb)[0] == 19690720) {
                        return (100 * noun + verb).ToString();
                    }
                }
            }
            return null;
        }

        int[] Run(int[] code, int noun, int verb) {
            code[1] = noun;
            code[2] = verb;
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
            return code;
        }

        class SomethingWentWrongException : Exception {

        }
    }
}
