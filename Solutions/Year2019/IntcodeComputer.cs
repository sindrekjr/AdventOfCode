using System; 
using System.Collections.Generic; 
using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class IntcodeComputer {

        int[] intcode, memory; 
        Queue<int> inputs; 

        public List<int> Output;

        public IntcodeComputer(int[] intcode) {
            this.intcode = intcode;
            Initialize(); 
        }

        public IntcodeComputer Initialize(int? noun = null, int? verb = null) {
            memory = new int[intcode.Length];
            intcode.CopyTo(memory, 0);
            if(noun != null) memory[1] = noun.Value; 
            if(verb != null) memory[2] = verb.Value; 
            inputs = new Queue<int>(); 
            Output = new List<int>(); 
            return this; 
        } 

        public IntcodeComputer Input(params int[] inp) {
            Initialize(); 
            foreach(int i in inp) inputs.Enqueue(i); 
            return this; 
        }

        public int[] Run(int? input = null) {
            if(input != null) Input(input.Value); 
            int i = 0; 
            while(true) {
                (Mode[] modes, Opcode opcode) = ParseInstruction(memory[i]); 
                if(opcode == Opcode.Input) {
                    memory[memory[++i]] = inputs.Dequeue();
                } else if(opcode == Opcode.Output) {
                    Output.Add((modes[0] == Mode.Position) ? memory[memory[++i]] : memory[++i]);
                } else if(opcode == Opcode.Halt) {
                    break; 
                } else {
                    int val1 = (modes[0] == Mode.Position) ? memory[memory[++i]] : memory[++i];
                    int val2 = (modes[1] == Mode.Position) ? memory[memory[++i]] : memory[++i];
                    switch(opcode) {
                        case Opcode.Add: 
                            if(modes[2] == Mode.Position) {
                                memory[memory[++i]] = val1 + val2; 
                            } else {
                                memory[++i] = val1 + val2; 
                            }
                            break; 
                        case Opcode.Multiply: 
                            if(modes[2] == Mode.Position) {
                                memory[memory[++i]] = val1 * val2; 
                            } else {
                                memory[++i] = val1 * val2; 
                            }
                            break;
                        case Opcode.JumpTrue:
                            if(val1 != 0) {
                                i = val2; 
                                continue; 
                            }
                            break; 
                        case Opcode.JumpFalse:
                            if(val1 == 0) {
                                i = val2; 
                                continue; 
                            }
                            break; 
                        case Opcode.Lt:
                            if(modes[2] == Mode.Position) {
                                memory[memory[++i]] = (val1 < val2) ? 1 : 0; 
                            } else {
                                memory[++i] = (val1 < val2) ? 1 : 0; 
                            }
                            break; 
                        case Opcode.Eq:
                            if(modes[2] == Mode.Position) {
                                memory[memory[++i]] = (val1 == val2) ? 1 : 0; 
                            } else {
                                memory[++i] = (val1 == val2) ? 1 : 0; 
                            }
                            break; 
                        default: 
                            throw new SomethingWentWrongException();
                    }
                }
                i++; 
            }
            return memory; 
        }

        public int Diagnose() => Output.Last(); 

        (Mode[] modes, Opcode opcode) ParseInstruction(int instruction) {
            return (
                instruction.ToString("D5").Remove(3).Reverse()
                    .Select<char, Mode>(c => Enum.Parse<Mode>(c.ToString()))
                    .ToArray(),
                (Opcode) (instruction % 100)
            );
        }

        enum Opcode { Add = 1, Multiply, Input, Output, JumpTrue, JumpFalse, Lt, Eq, Halt = 99 }
        enum Mode { Position, Immediate }
    }

    class SomethingWentWrongException : Exception {

    }
}