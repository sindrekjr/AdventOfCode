using System; 
using System.Collections.Generic; 
using System.Linq; 

namespace AdventOfCode.Solutions.Year2019 {

    class IntcodeComputer {

        readonly int[] intcode; 
        bool initialized = false; 
        
        public bool Paused { get; private set; }
        public int[] Memory { get; private set; }
        public Queue<int> Input { get; private set; }
        public Queue<int> Output { get; private set; }

        public IntcodeComputer(int[] intcode) {
            this.intcode = intcode;
        }

        public IntcodeComputer Initialize(int? noun = null, int? verb = null) {
            Memory = new int[intcode.Length];
            intcode.CopyTo(Memory, 0);
            if(noun != null) Memory[1] = noun.Value; 
            if(verb != null) Memory[2] = verb.Value; 
            Input = new Queue<int>(); 
            Output = new Queue<int>(); 
            initialized = true; 
            return this; 
        } 

        public IntcodeComputer InputSequence(params int[] inp) {
            Initialize(); 
            foreach(int i in inp) Input.Enqueue(i); 
            return this; 
        }

        public IntcodeComputer Run(int? inp = null) {
            if(inp != null) InputSequence(inp.Value); 
            if(!initialized) Initialize(); 
            Paused = false; 
            int i = 0; 
            int rel = 0; 
            while(true) {
                (Mode[] modes, Opcode opcode) = ParseInstruction(Memory[i]); 
                if(opcode == Opcode.Input) {
                    if(Input.Count > 0) {
                        Memory[modes[0] switch {
                            Mode.Position => Memory[++i],
                            Mode.Immediate => ++i,
                            Mode.Relative => Memory[++i] + rel,
                            _ => throw new SomethingWentWrongException()
                        }] = Input.Dequeue(); 
                    } else {
                        Paused = true; 
                        return this; 
                    }
                } else if(opcode == Opcode.Output) {
                    Output.Enqueue(modes[0] switch {
                        Mode.Position => Memory[Memory[++i]],
                        Mode.Immediate => Memory[++i],
                        Mode.Relative => Memory[Memory[++i] + rel],
                        _ => throw new SomethingWentWrongException()
                    });
                } else if(opcode == Opcode.Adjust) {
                    rel += modes[0] switch {
                        Mode.Position => Memory[Memory[++i]],
                        Mode.Immediate => Memory[++i],
                        Mode.Relative => Memory[Memory[++i] + rel],
                        _ => throw new SomethingWentWrongException()
                    };
                } else if(opcode == Opcode.Halt) {
                    return this; 
                } else {
                    int val1 = modes[0] switch {
                        Mode.Position => Memory[Memory[++i]],
                        Mode.Immediate => Memory[++i],
                        Mode.Relative => Memory[Memory[++i] + rel],
                        _ => throw new SomethingWentWrongException()
                    };
                    int val2 = modes[1] switch {
                        Mode.Position => Memory[Memory[++i]],
                        Mode.Immediate => Memory[++i],
                        Mode.Relative => Memory[Memory[++i] + rel],
                        _ => throw new SomethingWentWrongException()
                    };
                    switch(opcode) {
                        case Opcode.Add: 
                            Memory[modes[2] switch {
                                Mode.Position => Memory[++i],
                                Mode.Immediate => ++i,
                                Mode.Relative => Memory[++i] + rel,
                                _ => throw new SomethingWentWrongException()
                            }] = val1 + val2; 
                            break; 
                        case Opcode.Multiply: 
                            Memory[modes[2] switch {
                                Mode.Position => Memory[++i],
                                Mode.Immediate => ++i,
                                Mode.Relative => Memory[++i] + rel,
                                _ => throw new SomethingWentWrongException()
                            }] = val1 * val2; 
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
                            Memory[modes[2] switch {
                                Mode.Position => Memory[++i],
                                Mode.Immediate => ++i,
                                Mode.Relative => Memory[++i] + rel,
                                _ => throw new SomethingWentWrongException()
                            }] = (val1 < val2) ? 1 : 0; 
                            break; 
                        case Opcode.Eq:
                            Memory[modes[2] switch {
                                Mode.Position => Memory[++i],
                                Mode.Immediate => ++i,
                                Mode.Relative => Memory[++i] + rel,
                                _ => throw new SomethingWentWrongException()
                            }] = (val1 == val2) ? 1 : 0; 
                            break; 
                        default: 
                            throw new SomethingWentWrongException();
                    }
                }
                i++; 
            }
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

        enum Opcode { Add = 1, Multiply, Input, Output, JumpTrue, JumpFalse, Lt, Eq, Adjust, Halt = 99 }
        enum Mode { Position, Immediate, Relative }
    }

    class SomethingWentWrongException : Exception {

    }
}
