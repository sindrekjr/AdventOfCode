using System; 
using System.Collections.Generic; 
using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {
    class IntcodeComputer {

        bool initialized = false; 
        readonly int[] intcode; 
        int[] memory; 

        public List<int> Output;

        public IntcodeComputer(int[] intcode) {
            this.intcode = intcode;
        }

        public void Initialize(int? noun = null, int? verb = null) {
            memory = new int[intcode.Length];
            intcode.CopyTo(memory, 0);
            if(noun != null) memory[1] = noun.Value; 
            if(verb != null) memory[2] = verb.Value; 

            Output = new List<int>(); 
            initialized = true; 
        } 

        public int[] Run(int? input = null) {
            if(!initialized) Initialize(); 
            for(int i = 0; i < memory.Length; i++) {
                (string modes, Opcode opcode) = ParseInstruction(memory[i]); 
                //Console.WriteLine($"[{i}]  Opcode: {opcode}, Modes: {modes[0]}, {modes[1]}, {modes[2]}");
                string debug = $"[{i}] {memory[i]}  :: {opcode}"; 
                int output;
                if(opcode == Opcode.Add) {
                    int val1 = (modes[0] == '0') ? memory[memory[++i]] : memory[++i];
                    int val2 = (modes[1] == '0') ? memory[memory[++i]] : memory[++i];
                    output = val1 + val2; 
                } else if(opcode == Opcode.Multiply) {
                    int val1 = (modes[0] == '0') ? memory[memory[++i]] : memory[++i];
                    int val2 = (modes[1] == '0') ? memory[memory[++i]] : memory[++i];
                    output = val1 * val2;
                } else if(opcode == Opcode.Input) {
                    output = input.Value;
                } else if(opcode == Opcode.Output) {
                    Output.Add((modes[0] == '0') ? memory[memory[++i]] : memory[++i]);
                    continue; 
                } else if(opcode == Opcode.Halt) {
                    break;
                } else {
                    throw new SomethingWentWrongException();
                }
                memory[memory[++i]] = output;
            }
            return memory;
        }

        public int Diagnose() {
            return Output.Last(); 
        }

        (string modes, Opcode opcode) ParseInstruction(int instruction) {
            return (
                instruction.ToString("D5").Remove(3).Reverse(),
                (Opcode) (instruction % 100)
            );
        }

        enum Opcode { Add = 1, Multiply = 2, Input = 3, Output = 4, Halt = 99 }
        enum Mode { Position, Immediate }
    }

    class SomethingWentWrongException : Exception {

    }
}