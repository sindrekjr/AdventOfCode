using System; 
using System.Collections.Generic; 
using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class IntcodeComputer {

        readonly int[] intcode; 
        
        public bool Paused { get; private set; }
        public int[] Memory { get; private set; }
        public Queue<int> Input { get; private set; }
        public Queue<int> Output { get; private set; }

        public IntcodeComputer(int[] intcode) {
            this.intcode = intcode;
            Initialize(); 
        }

        public IntcodeComputer Initialize(int? noun = null, int? verb = null) {
            Memory = new int[intcode.Length];
            intcode.CopyTo(Memory, 0);
            if(noun != null) Memory[1] = noun.Value; 
            if(verb != null) Memory[2] = verb.Value; 
            Input = new Queue<int>(); 
            Output = new Queue<int>(); 
            return this; 
        } 

        public IntcodeComputer InputSequence(params int[] inp) {
            Initialize(); 
            foreach(int i in inp) Input.Enqueue(i); 
            return this; 
        }

        public IntcodeComputer Run(int? inp = null) {
            if(inp != null) InputSequence(inp.Value); 
            Paused = false; 
            int i = 0; 
            while(true) {
                (Mode[] modes, Opcode opcode) = ParseInstruction(Memory[i]); 
                if(opcode == Opcode.Input) {
                    if(Input.Count > 0) {
                        if(modes[0] == Mode.Position) {
                            Memory[Memory[++i]] = Input.Dequeue();
                        } else {
                            Memory[++i] = Input.Dequeue();
                        }
                    } else {
                        Paused = true; 
                        break; 
                    }
                } else if(opcode == Opcode.Output) {
                    Output.Enqueue((modes[0] == Mode.Position) ? Memory[Memory[++i]] : Memory[++i]);
                } else if(opcode == Opcode.Halt) {
                    break; 
                } else {
                    int val1 = (modes[0] == Mode.Position) ? Memory[Memory[++i]] : Memory[++i];
                    int val2 = (modes[1] == Mode.Position) ? Memory[Memory[++i]] : Memory[++i];
                    switch(opcode) {
                        case Opcode.Add: 
                            if(modes[2] == Mode.Position) {
                                Memory[Memory[++i]] = val1 + val2; 
                            } else {
                                Memory[++i] = val1 + val2; 
                            }
                            break; 
                        case Opcode.Multiply: 
                            if(modes[2] == Mode.Position) {
                                Memory[Memory[++i]] = val1 * val2; 
                            } else {
                                Memory[++i] = val1 * val2; 
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
                                Memory[Memory[++i]] = (val1 < val2) ? 1 : 0; 
                            } else {
                                Memory[++i] = (val1 < val2) ? 1 : 0; 
                            }
                            break; 
                        case Opcode.Eq:
                            if(modes[2] == Mode.Position) {
                                Memory[Memory[++i]] = (val1 == val2) ? 1 : 0; 
                            } else {
                                Memory[++i] = (val1 == val2) ? 1 : 0; 
                            }
                            break; 
                        default: 
                            throw new SomethingWentWrongException();
                    }
                }
                i++; 
            }
            return this; 
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
